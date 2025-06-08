# Distributed Text Analyzer System in C#

##  Project Overview

This project is a distributed text analysis system developed using C#. It includes three main components:

- **Master**: Coordinates communication and collects results.
- **ScannerA**: Scans and analyzes `.txt` files from a specified directory.
- **ScannerB**: Performs the same role as ScannerA but in parallel, possibly in a different directory or machine.
- Communication is handled through **named pipes** for efficient inter-process communication (IPC).

---

##  Folder Structure

**Master**:
   - Listens on named pipes (`ScannerA_pipe` and `ScannerB_pipe`) using multithreading.
   - Aggregates word count results received from both ScannerA and ScannerB.

2. **ScannerA / ScannerB**:
   - Read all `.txt` files in their specified directory.
   - Count the occurrences of each word.
   - Serialize the results and send them to Master via named pipes.

---

##  How to Run

1. **Create `.txt` Files**:
   - Add one or more `.txt` files in folders for ScannerA and ScannerB to process.

2. **Run Order**:
   - First, start the **Master** application.
   - Then start **ScannerA** and **ScannerB**.

3. **Expected Output**:
   - Console shows connection messages.
   - The Master receives, merges, and displays word counts from both scanners.

---

##  Technologies Used

- **.NET Console Applications**
- **C# 10 / C# 11**
- **Named Pipes (System.IO.Pipes)**
- **Multithreading (System.Threading)**
- **StreamReader/StreamWriter for I/O**

---

##  Testing

- Test cases involved multiple `.txt` files with mixed content (words, punctuation).
- Verified both individual scanner output and combined result at the Master side.
- Final test confirms that both ScannerA and ScannerB connected successfully to Master and transmitted data as expected.

---

##  Final Output

- Both **ScannerA** and **ScannerB** connected with **Master** successfully.
- Master displayed the final aggregated word count result from both scanners.
- The system ran smoothly and passed all test scenarios.
