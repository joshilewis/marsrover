# Mars Rover

## A small codebase to demonstrate how I approach coding problems. 

The Mars Rover accepts instructions as a text file in a specific format. The Rover prints out its final state when it is finished processing the input from the file. Its state consists of its coordinates the direction it is facing.

### How to run
To execute, run `rover.exe` from the command line. You will be asked to type in the name of the input file (including path). If there are any issues with the input file the Rover will tell you. If the Rover encounters any problems while processing the instructions in the input file, it will yell you. If all goes well, the Rover will tell you where it ends up and in what direction it is facing.

### Approach
The approach taken uses Behaviour-Driven Development and Specification with Example. During development I commit frequently, often at very fine-grained intervals. This allows a reader to follow my thought process and code changes. The steps used can be summarised as follows:  
 1. Write the executable specification for `Example Journey 1` (mentioned below), which is supplied with the problem. The specification is expressed from the point of view of a user external to the system, i.e. as far as possible using the real user interface of the system. To this end, the executable specifications use real files, instead of passing in strings.
 2. Write the code for the system such that it satisfies the executable specification in `1.` above. The goal here is to get to a functionally correct (as described by the spec) system without paying *primary* attention to the design of the system. This approach is based on the value statement: `I have come to value functional correctness (as expected by the user) over 'design'`. 
 3. Once the system satisfies the spec, I brainstormed other specifications that the system should satisfy, such as those mentioned in **File Specifications** below, as well as 'out of bounds' specifications. These specs were then written in TDD fashion, i.e. Red-Green-Refactor', on a spec-by-spec basis. During this phase, both the production and spec code are refactored as and when appropriate.
 4. Once the system satisfied all specs, I remodeled the system design through well known refacorings, relying on tools (Visual Studio and Resharper) as much as possible to do the actual code changes. Example remodelings include extracting [RoverState](https://github.com/joshilewis/marsrover/commit/31acef11934401f3a2852e8b97f404dd8c060258) and [RoverFileReader](https://github.com/joshilewis/marsrover/commit/f68277d8d8a531fa812bef695db5f18ff0ceb4e2) from `Rover`. These abstractions were chosen because they provide more encapsulation and cohesion of the elements in the design. For example, the mechanisms of moving the rover and changing the rover's direction are now both encapsulated within `RoverState`. 
 5. Once happier with the design of the solution, and the specs, I shifted attention to the actual executable. At this point I realised that the decision made earlier to bubble exceptions beyond `Rover` would not work well for a shell executable, and a better pattern would be to return a message for every type of input. This precipitated adding exception handling to `Rover` for exceptions raised by `RoverFileReader`, and lower-level methods within `Rover` itself. In addition, the specs were changed to simply verify that the output message expected for a particular input file was actually returned, as opposed to the verification of thrown exceptions that was chosen previously. [This commit](https://github.com/joshilewis/marsrover/commit/8b6e1f09992bb0347345f376416698a28c23e6ff) refers.
 6. The next piece of work was to consolidate some of the specification boilerplate code so as to reduce duplication. `RoverSpec` was extracted as a superclass, and then used by `MovingSpecs` and `FileSpecs`.
 7. The last major change was to move the classes modelling the domain into the `Model` assembly and namespace, from the 'executable' assembly. This makes a clear distinction between the 3 code areas, namely: executable, model and specs. This refactoring allows for reusing the model code in different execution or deployment scenarios.

 ### Notes
  * There are no specs or tests for components at a lower level than `Rover`
  * I was able to remodel the system, as described in `4.` above, *without modifying the specs*. I could make any change I wished with the safety net of knowing I had not changed externally observable behaviour.
  * I use a tool called [NCrunch](http://www.ncrunch.net/) which continuously compiles my code and executes my executable specs *while I type*. This provides an incredibly fast feedback loop about whether the system satisfies the specs. This feedback is on the order of seconds.
  * I deliberately focused on achieving functional correctness and conformance to spec *before* focusing on design. In my experience developers jump to solution designs far too quickly. They make long-lasting decisions at the beginning of the journey, when they know the least about the problem and the solution. Thus these decisions are based on many assumptions. Most of the assumptions will be proven untrue at some point, we're just not sure when. I also believe most developers over emphasize 'good design'. In my experience, it is better to keep things as simple as possible for as long as possible, and let designs emerge over time. This is enabled by the BDD approach described above: regardless of the design of the system, it can be guaranteed at any and every point that the system works as expected. This allows a lot of freedom to experiment and evolve the system design.
  * I considered using either the State Pattern or Strategy Pattern to encapsulate direction changes and movements. For this particular problem either of these Patterns would be overkill, and would introduce more complexity and cognitive load than they would reduce. In real-world scenarios with similar problems, one of these Patterns would be appropriate.
  * One of the trade-offs I made was to decide which element was responsible for calculating whether the rover had moved out of the zone. Tihs is a trade-off because it involves 2 pieces of information, the rover's current state, and the size of the zone. I decided to expose `RoverState.X`, `RoverState.Y` and `RoverState.Direction`, and let `Rover` [do the out-of-zone calculations](https://github.com/joshilewis/marsrover/blob/31acef11934401f3a2852e8b97f404dd8c060258/Console/Rover.cs#L93-L96) as I consider this a 'cleaner' design than exposing `RoverState` to the zone size.
  * Initially I used the term 'bounds'. I subsequently used 'zones', which is used in the original problem description. There is a large amount of code which still uses 'bounds'. This should be changed soon.

### Specifications
All specifications of expected Rover behaviour consist of 3 parts:
 * Name: A short name of the scenario
 * Input file: A sample input file (and contents) which represents the scenario
 * Output message: The message the Rover is expected to return for this scenario.

The specifications for the Rover are split into two categories: File Specifications and Movement Specifications. 

#### File Specifications
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

#### Movement Specifications
The movement specifications describe the outcome of the Rover's journey based on the instructions in the input file. The following cases are covered:  

**Scenario**|**Input File Contents**|**Expected Output**  
---|---|---  
Move East of zone|1 1<br>0 0 E<br>M|Rover would move East out of the zone
Move North of zone|1 1<br>0 0 N<br>M|Rover would move North out of the zone
Move South of zone|1 1<br>0 0 S<br>M|Rover would move South out of the zone
Move West of zone|1 1<br>0 0 W<br>M|Rover would move West out of the zone
Example journey 1|8 8<br>1 2 E<br>MMLMRMMRRMML|3 3 S
