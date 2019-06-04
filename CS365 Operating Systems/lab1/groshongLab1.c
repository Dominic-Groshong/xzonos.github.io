/*==============================================================================
  @file         groshongLab1.c
  @author       Dominic Groshong
  @date         April 20, 2019
  @brief        Create a simple shell program in c.
  @sources      https://brennan.io/2015/01/16/write-a-shell-in-c/
  @sources      https://www.geeksforgeeks.org/making-linux-shell-c/
==============================================================================*/

#include <sys/wait.h>
#include <unistd.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>

#include <dirent.h>
#include <fnmatch.h>
#include <sys/types.h>
#include <sys/stat.h>



// Command method declarations
int changeDirectoryCommand(char **args);
int helpCommand(char **args);
int exitCommand(char **args);
int cdCommand(char **args);
int pidCommand(char **args);

// List of avalible commands
char *stringCommands[] = {
  "help",
  "exit",
  "cd",
  "pid"
};

// List of corresponding methods that attach to appropriately parsed strings.
int (*functionNames[]) (char **) = {
  &helpCommand,
  &exitCommand,
  &cdCommand,
  &pidCommand
};

int numberOfCommands(){
  return sizeof(stringCommands) / sizeof(char *);
}

//
// Methods
//

// Help Command: prints a list of the avalible commands.
int helpCommand(char **args){
  int i;
  printf("===================================================\n");
  printf(" There are several commands built into this shell: \n");
  printf("===================================================\n");

  for (i = 0; i < numberOfCommands(); i++){
    printf("  %s\n", stringCommands[i]);
  }

  return 1;
}

int cdCommand(char **args)
{
  if (args[1] == NULL) {
    fprintf(stderr, "error: expected argument to \"cd\"\n");
  } else {
    if (chdir(args[1]) != 0) {
      perror("error");
    }

    char cwd[1024]; 
    getcwd(cwd, sizeof(cwd)); 
    printf("Dir: %s \n", cwd); 
  }
  return 1;
}

int pidCommand(char **args)
{
  int id = getpid();
  printf("Current PID: %d\n", id );
  return 1;
}

// Exit Command: exits the shell and closes the terminal.
int exitCommand(char **args){
    return 0;
}

// Return the running processes.
int filter(const struct dirent *dir){
    return !fnmatch("[1-9]*", dir->d_name, 0);
}

// Launch program.
int launcher(char **args){
    pid_t pid;
    int status;

    pid = fork();
    if (pid == 0) {
        // Child process
        if (execvp(args[0], args) == -1) {
            perror("error");
        }
    exit(EXIT_FAILURE);
    } else if (pid < 0) {
        // Error forking
        perror("error");
    } else {
        // Parent process
        do {
            waitpid(pid, &status, WUNTRACED);
        } while (!WIFEXITED(status) && !WIFSIGNALED(status));
    }

    return 1;
}

// Execute shell built-in or launch program.
int execute(char **args){
    int i;

    if (args[0] == NULL) {
        // An empty command was entered.
        printf("error: command cannot be empty.\n");
        return 1;
    }

    for (i = 0; i < numberOfCommands(); i++) {
        if (strcmp(args[0], stringCommands[i]) == 0) {
            return (*functionNames[i])(args);
        }
    }

    return launcher(args);
}

// Read Line: Reads in the user inputed char and parses it.
char *readLine(void){
    char *line = NULL;
    ssize_t bufsize = 0;
    getline(&line, &bufsize, stdin);
    return line;
}

// Constants used by splitLine
#define BUFFERSIZETOK 64
#define TOKDELIM " \t\r\n\a"

// Split Line: splits the line into tokens (which apparently is important).
char **splitLine(char *line){

    // Internal variables.
    int bufsize = BUFFERSIZETOK, position = 0;
    char **tokens = malloc(bufsize * sizeof(char*)); // allocates space to store command.
    char *token, **tokensBackup;

    // error if no tokens.
    if (!tokens) {
        fprintf(stderr, "allocation error\n");
        exit(EXIT_FAILURE);
    }

    token = strtok(line, TOKDELIM);
    while (token != NULL) {
        tokens[position] = token;
        position++;

        if (position >= bufsize) {
            bufsize += BUFFERSIZETOK;
            tokensBackup = tokens;
            tokens = realloc(tokens, bufsize * sizeof(char*));
            if (!tokens) {
                free(tokensBackup);
                fprintf(stderr, "allocation error\n");
                exit(EXIT_FAILURE);
            }
        }

        token = strtok(NULL, TOKDELIM);
    }

    tokens[position] = NULL;
    return tokens;
}

// Shell driver. Calls the methods to read in lines and exicute commands.
void shell(void)
{
  char *line;
  char **args;
  int status;
  char* username = getenv("USER");

  do {
    printf("%s@ubuntu: ", username);
    line = readLine();
    args = splitLine(line);
    status = execute(args);

    free(line);
    free(args);
  } while (status);
}

// Main
int main(int argc, char **argv)
{
  shell();
  return EXIT_SUCCESS;
}
