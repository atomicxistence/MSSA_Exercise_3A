# Taskr
> MSSA Exercise 3A: Long Task List Console Application

![Application Demo](https://github.com/atomicxistence/MSSA_Exercise_3A/blob/master/SupportingDocuments/ApplicationDemo.gif)

This console application is a task tracking system using the [Simple Scanning](http://markforster.squarespace.com/blog/2017/12/2/simple-scanning-the-rules.html) technique by Mark Forster. The requirements for designing this app given by my MSSA instructor can be found [here](https://github.com/atomicxistence/MSSA_Exercise_3A/blob/master/SupportingDocuments/ApplicationRequirements.md).

---
### **Status**
This project is still in alpha and has no API support.  

---
### **Design**
I used lessons learned from my previous GUI-lite console implementations and built the GUI for this project from the ground up. I plan to abstract the GUI code from this project as a seperate library for future use.

Using the SOLID design principles, I created a library for the data containers and the logic to manipulate the data. Trying to seperate the console UI from logic as much as possible.

---
### **Use**
|Key | Function |
|----|----------|
|`UpArrow`| Scroll **up** through list|
|`DwnArrow`| Scroll **down** through list |
|`RightArrow`| Go to the **next** page |
|`LeftArrow`| Go to the **previous** page |
|`Return`| **Select** current task |
|`N`| Create a **new** task |
|`Escape`| **Quit** application |
