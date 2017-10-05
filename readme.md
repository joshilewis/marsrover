# Mars Rover

## A small codebase to demonstrate how I think about solving coding problems. 

The Mars Rover accepts instructions as a text file in a specific format. The Rover prints out its final state when it is finished processing the input from the file. Its state consists of its coordinates the direction it is facing.

### Execution
To execute, run `rover.exe` from the command line. You will be asked to type in the name of the input file (including path). If there are any issues with the input file the Rover will tell you. If the Rover encounters any problems while processing the instructions in the input file, it will yell you. If all goes well, the Rover will tell you where it ends up and in what direction it is facing.

### Approach


#### Specifications
All specifications of expected Rover behaviour consist of 3 parts:
 * Name: A short name of the scenario
 * Input file: A sample input file (and contents) which represents the scenario
 * Output message: The message the Rover is expected to return for this scenario.

The specifications for the Rover are split into two categories: File Specifications and Movement Specifications. 

##### File Specifications
The file specifications describe how the Rover should respond to issues with the input file. The following cases are covered:  

**Scenario**|**Input File Contents**|**Expected Output**  
---|---|---  
Non-existent File|\<invalid filename>|The specified file can't be found
Starting East of zone|8 8<br>8 2 E<br>M|Rover would start East of zone
Starting North of zone|8 8<br>1 8 E<br>M|Rover would start North of zone
Invalid starting direction|1 1<br>0 0 X<br>M|Invalid starting direction: X
Negative starting X|8 8<br>-1 2 E<br>M|Negative starting X
Negative starting Y|8 8<br>1 -2 E<br>M|Negative starting Y
Invalid Command|8 8<br>1 2 E<br>MMLMRXMRRMML|Invalid Command: X

##### Movement Specifications
The movement specifications describe the outcome of the Rover's journey based on the instructions in the input file. The following cases are covered:  

**Scenario**|**Input File Contents**|**Expected Output**  
---|---|---  
Move East of zone|1 1<br>0 0 E<br>M|Rover would move East out of the zone
Move North of zone|1 1<br>0 0 N<br>M|Rover would move North out of the zone
Move South of zone|1 1<br>0 0 S<br>M|Rover would move South out of the zone
Move West of zone|1 1<br>0 0 W<br>M|Rover would move West out of the zone
Example journey 1|8 8<br>1 2 E<br>MMLMRMMRRMML|3 3 S

