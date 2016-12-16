# catpictures

This Repository is a self learning tool to help understand the basics of multiple languages.

## Running these (Windows 10):

#### Python
Navigate to Python root folder then from command prompt run: `python main.py`

#### Golang
Navigate to `/src/wallpaper/` in the Golang folder then from command prompt run `go build`.
This will produce `wallpaper.exe` which can be executed from the command line.

#### .NET
Navigate to `wallpaper` in the the .NET folder and run `csc Program.cs`. This will
produce a Program.exe which can be executed from the command line.

#### NodeJS
From the root of the NodeJS folder simply run `npm install` once complete you can run `npm start` to execute the program.

#### Java
To compile and run from the windows CMD
First of all ensure you have an Enviromental variable PATH set to:
`C:\Program Files\Java\jdkxxx_xxx\bin` and a folder in C drive called git.
Compilation Example:
Navigate to `C:\git\catpictures\Java\src\com\cats\java`
Proceed to compile classes
EXAMPLE - `javac -cp "C:\git\catpictures\Java\lib\jna-4.2.2.jar;C:\git\catpictures\
Java\lib\jsoup-1.10.1.jar" *.java`
Upon compiling classes with external libraries referenced, program is ready to run
Navigate to `C:\git\pictures\Java\src`
Proceed to run program
EXAMPLE- `java -cp "C:\git\catpictures\Java\lib\jsoup-1.10.1.java;C:\gitcatpictures\Java\lib\jna-4.2.2.jar;" com/cats/java/Main

For ease and use in regards to compiling and running of Java code an IDE or build tool is recommended
however can be can be completed manually within the Windows command prompt as demonstraited above.

The purpose is the create the same end result from multiple different languages in order to:

* Learn
* Highlight differences


## The Goal?

Using each of the above languages *(See Folders)*:

* Grab a Random Github OctoCat picture from: https://octodex.github.com/
* Save it to disk
* Set it as the Wallpaper
