#ifndef STRING_TOOLS_H
#define STRING_TOOLS_H

#include <string>

void replaceSubstring(std::string &str, const std::string &target, const std::string &replacement) {
    size_t pos = 0;
    while ((pos = str.find(target, pos)) != std::string::npos) {
        str.replace(pos, target.length(), replacement);
        pos += replacement.length();
    }
}

void replaceFirstOccurrence(std::string &str, const std::string &oldSubstr, const std::string &newSubstr) {
    size_t pos = str.find(oldSubstr);
    if (pos != std::string::npos) {
        str.replace(pos, oldSubstr.length(), newSubstr);
    }
}

bool startsWith(const std::string& str, const std::string& prefix) {
    return str.find(prefix) == 0;
}

#endif