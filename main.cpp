#include <iostream>
#include <string>

#include "include/ccolors.h"        // Include the library I wrote that will allow the changing of console colours
#include "include/stools.h"         // Include the library I wrote that will allow the management of strings

using namespace std;

int main() {
    SetForegroundColor(YELLOW);
    // Initializing some stuffs
    cout << "Now loading portalinux..." << endl;
    SetForegroundColor(DEFAULT);
    string userInput = "This variable is empty right now!";
    string currentDirectory = "~"; // The currently selected directory
    SetForegroundColor(GREEN);
    cout << "Portalinux has finished initializing!" << endl;
    SetForegroundColor(DEFAULT);

    while (true) {
        PrintFormattedText("{GREEN}user@Portalinux{DEFAULT}:{BLUE}~{DEFAULT}$ ");
        getline(cin, userInput);

        if (userInput == "help") {
            cout << "This is a help menu!" << endl;
        }
        else if (userInput == "shutdown") {
            break;
        }
        else if (startsWith(userInput, "echo")) {
            if (startsWith(userInput, "echo ")) {
                replaceFirstOccurrence(userInput, "echo ", "");
                cout << userInput << endl;
            }
            else if (userInput == "echo") {
                cout << endl;
            }
            else {
                cout << userInput << ": command not found" << endl;
            }
        }
        else if (userInput == "ls") {
            PrintFormattedText("{BLUE}ExampleDirectory  {GREEN}ExampleApplication  {DEFAULT}file.txt");
            cout << endl;
        }
        else if (startsWith(userInput, "cat")) {
            if (startsWith(userInput, "cat ")) {
                replaceFirstOccurrence(userInput, "cat ", "");
                if (userInput == "file.txt") {
                    cout << "This is a really cool text file that has lots of really cool data in it!" << endl;
                    cout << endl;
                    cout << "This file has some cool things in it that can't be seen outside this program." << endl;
                    cout << endl;
                    cout << "Making it SUPER secret!" << endl;
                }
                else if (userInput == "ExampleDirectory") {
                    cout << "cat: " << userInput << ": Is a directory" << endl;
                }
                else if (userInput == "ExampleApplication") {
                    SetBackgroundColor(WHITE);
                    cout << "                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      " << endl;
                    SetBackgroundColor(DEFAULT);
                }
                else {
                    cout << "cat: " + userInput + ": No such file or directory" << endl;
                }
            }
            else {
                cout << "cat: not a file or directory" << endl;
            }
        }
        else {
            cout << userInput << ": command not found" << endl;
        }
    }

    return 0; // Exits out of the application
}