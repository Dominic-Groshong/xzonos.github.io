// CS315 Lab 3: C data types
// Author: Dominic Groshong
// Version: Febuary 17th, 2019

#include <stdio.h>
#include <stdlib.h>  // Added to fix EXIT_FAILURE & EXIT_SUCCESS
#include <unistd.h>  // Added for close function
#include <stdbool.h> // Allows bool functions

// Global Veriables ** MEETS ONE GLOBAL VERIABLE **
FILE * fp = NULL;

// Initialize the data into memory * fp ** INITALIZE FUCTION REQUIREMENT **
int initialize(int argc, char ** argv)
{
	// Open the file given on the command line
	if( argc != 2 )
	{
		printf( "Usage: %s filename.mp3\n", argv[0] );
		return(EXIT_FAILURE);
	}
	// Open the file.
	fp = fopen(argv[1], "rb");

	// Check if fp is empty, return error.
	if( fp == NULL )
	{
		printf( "Can't open file %s\n", argv[1] );
		return(EXIT_FAILURE);
	}
	// Everything worked
	return 0;
}

// Get the size of information stored at * fp
float getSize(){
	// Set size to 0
	float size = 0;

	// get the size of fp
	if( fp != NULL){
		fseek( fp, 0, SEEK_END );		// go to 0 bytes from the end
		size = ftell(fp);				// how far from the beginning?
		rewind(fp);						// go back to the beginning
	}
	return size;
}

// Read the info stored at * fp into * data adding constrants to prevent walking off edge of the array. ** READ FILE FUNCTION REQUIREMENT **
unsigned char *  readFile(FILE * input){
	float size = getSize();				// float set to the get size function

	long MAXSIZE = 10485760; 			// max file size allowed 	** CONSTRANTS DEFINED REQUIREMENT **
	long BYTE = 1048576;				// converter to mb  		** CONSTRANTS DEFINED REQUIREMENT **

	// If file too large report and deny
	if( size < 1 || size > MAXSIZE )
	{
		printf("File size is too large\n");
		close(0); // ** GOTO REMOVED REQUIREMENT **
	}

	// Print the file size.
	printf( "File size: %.2f MB\n", size/BYTE );
	// Allocate memory on the heap for a copy of the file
	unsigned char * data = (unsigned char *)malloc(size);
	// Read it into our block of memory
	size_t bytesRead = fread( data, sizeof(unsigned char), size, input );
	if( bytesRead != size )
	{
		printf( "Error reading file. Unexpected number of bytes read: %d\n",bytesRead ); // ** FILE SIZE TO TWO DECIMAL PLACES REQUIREMENT & PRINT OUT FILESIZE REQIREMENT **
		close(0); // ** GOTO REMOVED REQUIREMENT **
	}

	return data;
}
// Find the head of the potental MP3 for use in other functions
int findHead(unsigned char * data){
	float size = getSize();
	int i;
	int head = -1;
	// Update head
	while( head == -1) {
        for(i = 0; i < size; i = i + 1) {
            if ((data[i] == 0xFF) && ((data[i + 1] & 0xF0) == 0xF0)) {
                head = i;
                break;
            }
        }
    }

	return head;
}
// ** PRINT OUT INCLUDES IF FILE IS MP3 REQIREMENT **
bool isMP3(unsigned char * data, int head){
	// Provides location & data 4 bytes from the header
	unsigned char byte = (data[head + 1] & 0x0f);
	// Must be 0x0B to be MP3, return true.
	if(byte == 0x0B){
		printf("MPEG Layer 3: Yes\n");
		return true;
	}
	// Test failed, return false.
	printf("MPEG Layer 3: No\n");
	return false;
}
// ** PRINT OUT INCLUDES COPYRIGHT & ORIGINALITY REQIREMENT **
void isCopyrighted(unsigned char * data, int head) {
	// Get the byte where copyright information is stored
    unsigned char byte = (data[head + 3] & 0xF) >> 2;

	// Determine the copyright and duplication status from byte
    switch(byte) {
        case 0x00:
            printf("Copyright: No\nStatus: Duplicated\n");
            break;
        case 0x01:
            printf("Copyright: No\nStatus: Original\n");
            break;
        case 0x02:
            printf("Copyright: Yes\nStatus: Duplicate\n");
            break;
        case 0x03:
            printf("Copyright: Yes\nStatus: Original\n");
            break;
        default:
            printf("Copyright: Unknown\nStatus: Unknown");
    }
}
// ** PRINT OUT INCLUDES BITRATE REQIREMENT **
void bitrate(unsigned char * data, int head) {
    // Get the byte
    unsigned char byte = (data[head + 2] & 0xF0) >> 4;
	// Initialize bitrate to 0
    int bitrate = 0;
	// Determine bitrate
	switch(byte) {
        case 0 ... 5:
            bitrate = ((byte - 1) * 8) + 32;
			printf("Bitrate: %dkbps\n", bitrate);
            break;
        case 6 ... 9:
            bitrate = ((byte - 1) * 16);
			printf("Bitrate: %dkbps\n", bitrate);
            break;
        case 10 ... 13:
            bitrate = ((byte - 5) * 32);
			printf("Bitrate: %dkbps\n", bitrate);
            break;
		case 14:
            bitrate = 320;
			printf("Bitrate: %dkbps\n", bitrate);
            break;
        default:
            printf("Bitrate: Unknown\n");
    }
}
// ** PRINT OUT INCLUDES FREQUENCY REQIREMENT **
void frequency(unsigned char * data, int head) {
	// Get the byte where frequency information is stored
	unsigned char byte = (data[head + 2] & 0x0F) >> 2;
	// Initialize frequency to 0
	float frequency = 0.0;

    switch(byte) {
        case 0x00:
            frequency = 44100.0;
            break;
        case 0x01:
            frequency = 48000.0;
            break;
        case 0x02:
            frequency = 32000.0;
            break;
        default:
            printf("Frequency: Unknown\n");
    }

    printf("Frequency: %.1fkHz\n", frequency/1000.0);
}

int main( int argc, char ** argv )
{
	// Open the file in memory location.
	initialize(argc, argv);
	// Assign data the read file return also prints size.
    unsigned char * data = readFile(fp);
	// Find the head of data.
	int head = findHead(data);
	// If MP3, get frequency, bitrate, copyright, and originality.
	if(isMP3(data, head)){
		isCopyrighted(data, head);
		bitrate(data, head);
		frequency(data, head);
	}

	// ** MEMORY LEAK FIXED REQUIREMENT **
	free(data);
}
