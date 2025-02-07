#ifndef CONSOLE_COLORS_H
#define CONSOLE_COLORS_H

#include <iostream>
#include <string>
#include "stools.h"

enum ConsoleColour {
    DEFAULT,
    BLACK,
    RED,
    GREEN,
    YELLOW,
    BLUE,
    MAGENTA,
    CYAN,
    WHITE
};

std::string colorEscapes[9] = {
    "\033[39m",
    "\033[30m",
    "\033[31m",
    "\033[32m",
    "\033[33m",
    "\033[34m",
    "\033[35m",
    "\033[36m",
    "\033[37m"
};

void SetForegroundColour(ConsoleColour colour) {
    if (colour == DEFAULT) std::cout << "\033[39m";
    else if (colour == BLACK) std::cout << "\033[30m";
    else if (colour == RED) std::cout << "\033[31m";
    else if (colour == GREEN) std::cout << "\033[32m";
    else if (colour == YELLOW) std::cout << "\033[33m";
    else if (colour == BLUE) std::cout << "\033[34m";
    else if (colour == MAGENTA) std::cout << "\033[35m";
    else if (colour == CYAN) std::cout << "\033[36m";
    else if (colour == WHITE) std::cout << "\033[37m";
    else std::cout << "==INVALID-colour==";
}

void SetBackgroundColour(ConsoleColour colour) {
    if (colour == DEFAULT) {
        std::cout << "\033[49m";
    }
    else if (colour == BLACK) {
        std::cout << "\033[40m";
    }
    else if (colour == RED) {
        std::cout << "\033[41m";
    }
    else if (colour == GREEN) {
        std::cout << "\033[42m";
    }
    else if (colour == YELLOW) {
        std::cout << "\033[43m";
    }
    else if (colour == BLUE) {
        std::cout << "\033[44m";
    }
    else if (colour == MAGENTA) {
        std::cout << "\033[45m";
    }
    else if (colour == CYAN) {
        std::cout << "\033[46m";
    }
    else if (colour == WHITE) {
        std::cout << "\033[47m";
    }
    else {
        std::cout << "==INVALID-colour==";
    }
}

void SetForegroundColor(ConsoleColour color) { SetForegroundColour(color); }
void SetBackgroundColor(ConsoleColour color) { SetBackgroundColour(color); }

void PrintFormattedText(std::string input) {
    std::string text = input;
    replaceSubstring(text, "{DEFAULT}", colorEscapes[0]);
    replaceSubstring(text, "{BLACK}", colorEscapes[1]);
    replaceSubstring(text, "{RED}", colorEscapes[2]);
    replaceSubstring(text, "{GREEN}", colorEscapes[3]);
    replaceSubstring(text, "{YELLOW}", colorEscapes[4]);
    replaceSubstring(text, "{BLUE}", colorEscapes[5]);
    replaceSubstring(text, "{MAGENTA}", colorEscapes[6]);
    replaceSubstring(text, "{CYAN}", colorEscapes[7]);
    replaceSubstring(text, "{WHITE}", colorEscapes[8]);
    std::cout << text;
}

#endif