/* CS315 Lab 3: C data types */
/* Coded by Alex Bishop */

#include <stdio.h>
#include <stdlib.h>

/* Define statements */
#define MAX_SIZE 10485760
#define MB 1048576
/* Global variables */
FILE * fp;

/* Function prototypes */
int intialize(int argc, char ** argv);
float getSize(void);
unsigned char * readFile(void);
int readMP3(unsigned char * stream);
int checkIfMP3(unsigned char * stream, int head);
int getFrequency(unsigned char * stream, int head);
int getBitrate(unsigned char * stream, int head);
void checkCopy(unsigned char * stream, int head);


int main( int argc, char ** argv )
{
    /* Use intialize(), if it fails end execution */
    if (intialize(argc, argv) == 1) 
        return(EXIT_FAILURE);

    unsigned char * data = readFile();
	/* We now have a pointer to the first byte of data in a copy of the file, have fun
	 * unsigned char * data    <--- this is the pointer
     */


    /* main block of code for checking MP3 information */
    readMP3(data);
    
    /* Not anymore, we free that data pointer here, goodbye memory leak */
    free(data);	
	
	/* Clean up */
    fclose(fp);
	exit(EXIT_SUCCESS);		/* or return 0; */
}

/* Use: Fuction will return 1 if it fails to get file contents, else will return 0
 * Parameters:
 *  argc     - same as main argc, the number of arguments from program execution
 *  ** argv  - same as main ** argv, c-strings with names of execution arguments, argv[0] is program name
 * Return: status of storage in global variable FILE * fp
 */
int intialize(int argc, char ** argv)
{
	/* Open the file given on the command line */
	if( argc != 2 )
	{
		printf( "Usage: %s filename.mp3\n", argv[0] );
		return 1;
	}
	
	fp = fopen(argv[1], "rb");
	if( fp == NULL )
	{
		printf( "Can't open file %s\n", argv[1] );
		return 1;
	}
    return 0;
}

/* Use: return the size of the file, assuming that it has opened into fp
 * Parameters: none
 * Return: size of fp file in bytes
 */
float getSize(void) 
{
	/* How many bytes are there in the file?  If you know the OS you're
	 * on you can use a system API call to find out.  Here we use ANSI standard
	 * function calls.
     */
	float size = 0.0;
    /* Error checking, don't change size if file is empty */
    if ( fp != NULL ) {
	    fseek( fp, 0, SEEK_END );		/* go to 0 bytes from the end */
	    size = ftell(fp);				/* how far from the beginning? */
	    rewind(fp);                     /* go back to the beginning */
    }
    return size;
}

/* Use: read the file as bytes into a char buffer, supposing that file is assigned
 * Parameters: none
 * Return: NULL for an error state, otherwise it will return the unsigned char *
 */
unsigned char * readFile(void)
{
    /* Gets the size for the current file */
    float size = getSize();

	if( size < 1 || size > MAX_SIZE )
	{
		printf("File size is not within the allowed range\n"); 
		return NULL;
	}
	
	printf( "File size: %.2f MB\n", size/MB );
	/* Allocate memory on the heap for a copy of the file */
	unsigned char * data = (unsigned char *)malloc(size);
	/* Read it into our block of memory */
    /* Breaking down the fread call, it takes in data as the storage of read objects, using a byte as input size,
       size is the number of objects to be read, and finally fp is the stream we are reading from.
       It returns the number of objects (bytes) read to store in bytesRead WHILE actually storing them in data. 
    */
	size_t bytesRead = fread( data, sizeof(unsigned char), size, fp );
	if( bytesRead != size )
	{
		printf( "Error reading file. Unexpected number of bytes read: %d\n",bytesRead );
		return NULL;
	}

    /* Return unsigned char * data*/
    return data;   
}

/* Use: Read the unsigned char * object to get information about it
 * Parameters: unsighed char * stream - bytes stream 
 * Return: 0 in case of success or 1 in case of failure
 */
int readMP3(unsigned char * stream) {
    /* get size */
    float size = getSize();
    int i;
    int head = -1;
    
    /* Code here is for finding the first instance of a MP3 Header */
    /* while header value not assigned to be valid */
    while( head == -1) {
         /* Loop for file size */
        for(i = 0; i < size; i = i + 1) {
            /* If signature FFF is found, ie the Sync word */
            if ((stream[i] == 0xFF) && ((stream[i + 1] & 0xF0) == 0xF0)) {
                /* Print where we found it and save the location of this first header, then exit for loop */
                printf("Found valid Header with syncword \n");
                /* printf("Header is: %x%x%x%x \n", stream[i], stream[i + 1], stream[i + 2], stream[i + 3]); */
                head = i;
                /* printf("Header is at %i position in the byte stream \n", head); */
                break;
            }
        }
    }

    /* Check if the ile is MPEG Layer 3, exiting this functin if not */
    if (checkIfMP3(stream, head) > 0) {
        printf("MPEG Typing invalid, exiting...");
        return 1;
    }

    /* Do all other MP3 checks based on header */
    getBitrate(stream, head);
    getFrequency(stream, head);
    checkCopy(stream, head);
    return 0;
}

/* Use: Check if the char stream is a valid MP3
 * Parameters: unsigned char *stream - holds the char stream with header
 *             head - index of the header in stream
 * Return: 0 if pass ie (it is MPEG layer 3) and 1 if fail ie (it is not MPEG layer 3)
 */
int checkIfMP3(unsigned char *stream, int head) {
    
    /*This is a bitmask to just check 13th to 16th bits, ie the version layer and error protection */
    /*By using ( Byte & 0x0F ) we will only see what is located in the F of 0X0F or the 4 byte from the header */
    unsigned char byte = (stream[head + 1] & 0x0F);
    /* Via documentation, I know the bitmask must pass the value 0x0B to be correct */
    if(byte == 0x0B) {
        printf("File is MPEG Layer 3\n");
    }
    else {
        printf("File is not MPEG Layer 3\n");
        return 1;
    }

    return 0;
}

/* Use: Get the frequency from the header of a unsigned char * stream of bytes
 * Parameters:  unsigned char *stream - holds the char stream with header
 *              head - index of the header in stream
 * Return: frequency in kHz or error case
 */
int getFrequency(unsigned char * stream, int head) {
    /*This is a bit mask of the 25th to 26th bits, looking at specifically the frequency */
    /*Since bits 25 and 26 are the top bits of the lower hex, we shift them 2 places*/ 
    unsigned char byte = (stream[head + 2] & 0x0F) >> 2;
    /* Store frequency value */
    int freq = 0;
    
    /* 00 == 44100, 01 == 48000, 10 == 32000 */
    switch(byte) {
        case 0x00:
            freq = 44100;
            break;
        case 0x01:
            freq = 48000;
            break;
        case 0x02:
            freq = 32000;
            break;
        default:
            printf("Frequency is Unknown\n");
            return -1;
    }
	/* Moves freq 3 plaves and prints as decimal to 1st place*/
    printf("Frequency is at %.1fkHz\n", (float) freq/(float) 1000);
    
    return freq;
}

/* Use: Get the bitrate from the header of a unsigned char * stream of bytes
 * Parameters:  unsigned char *stream - holds the char stream with header
 *              head - index of the header in stream
 * Return: bitrate in kps or error case
 */
int getBitrate(unsigned char * stream, int head) { 
    /*This is a bit mask of the 17th to 20th bits, looking at specifically the bitrate */
    unsigned char byte = (stream[head + 2] & 0xF0);
    /* Store bitrate value */
    int rate = 0;

    /* Here I right shift the bit pattern by 4, allowing it in the 1st place */
    byte = byte >> 4;

    /* if not zero ie not error */
    /* loop is set up to avoid 0 and F cases */
    if (byte > 0) {
        /* 32, 40, 48, 56, 64 */
        if (byte <= 5) {
            rate = ((byte - 1) * 8) + 32;
        }
        /* 80, 96, 112, 128 */
        else if (byte <= 9) {
            rate = ((byte - 1) * 16);
        }
        /* 160, 192, 224, 256*/
        else if (byte <= 13) {
            rate = ((byte - 5) * 32);
        }
        /* 320 */
        else if(byte = 14) {
            rate = 320;
        }
     }

    /* print if valid */
    if (rate > 0) {
        printf("Bitrate is %dkbps\n", rate);
    } 
    else {
        printf("Invalid bits in bitrate\n");
    }
    
    return rate;
}

/* Use: Get the copyright info from the header of a unsigned char * stream of bytes and prints it
 * Parameters:  unsigned char *stream - holds the char stream with header
 *              head - index of the header in stream
 */
void checkCopy(unsigned char * stream, int head) {
    /* Bitmask for getting copyright status*/
    /* We shift things into the lowest order place for readable switch cases */
    unsigned char byte = (stream[head + 3] & 0xF) >> 2;
    
    switch(byte) {
        case 0x00:
            printf("MP3 is not copyrighted, and is a copy of the original media\n");
            break;
        case 0x01:
            printf("MP3 is not copyrighted, and is the original media\n");
            break;
        case 0x02:
            printf("MP3 is copyrighted, and is a copy of the original media\n");
            break;
        case 0x03:
            printf("MP3 is copyrighted, and is the original media\n");
            break;
        default:
            printf("Status of copyright and media is unknown");
    }
}
