# Domino
Domino is a file watcher that runs a custom script when changes occur.

## Installation



## Commands

### Start

`domino start {scriptName}`

This command will start the file watcher that will trigger the script on file changes in the current directory (and subdirectories).

### Ignore

`domino ignore {filePattern}`

This command will create a `.dominoignore` file in the current directory and add the specified file pattern to the file. 

