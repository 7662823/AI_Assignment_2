              _____       _               _______                  
             |_   _|     | |             |__   __|                 
               | |  _ __ | |_ _ __ ___      | | ___                
               | | | '_ \| __| '__/ _ \     | |/ _ \               
              _| |_| | | | |_| | | (_) |    | | (_) |              
             |_____|_| |_|\__|_|_ \___/     |_|\___/             
               /\        | | (_)/ _(_)    (_)     | |             
              /  \   _ __| |_ _| |_ _  ___ _  __ _| |            
             / /\ \ | '__| __| |  _| |/ __| |/ _` | |            
            / ____ \| |  | |_| | | | | (__| | (_| | |            
      _____/_/___ \_\_| _ \__|_|_|_|_|\___|_|\__,_|_|            
     |_   _|     | |     | | (_)                           
       | |  _ __ | |_ ___| | |_  __ _  ___ _ __   ___ ___  
       | | | '_ \| __/ _ \ | | |/ _` |/ _ \ '_ \ / __/ _ \ 
      _| |_| | | | ||  __/ | | | (_| |  __/ | | | (_|  __/ 
     |_____|_| |_|\__\___|_|_|_|\__, |\___|_| |_|\___\___| 
                                  _/ |                     
                                 |___/           
      __   __     __              ___      ___    ___       __  
 /\  /__` /__` | / _` |\ |  |\/| |__  |\ |  |      |  |  | /  \ 
/~~\ .__/ .__/ | \__> | \|  |  | |___ | \|  |      |  |/\| \__/ 
                    ___  ___  __   ___       __   ___           
            | |\ | |__  |__  |__) |__  |\ | /  ` |__            
            | | \| |    |___ |  \ |___ | \| \__, |___           
                   ___       __          ___                    
                  |__  |\ | / _` | |\ | |__                     
                  |___ | \| \__> | | \| |___                                                    
                                                  
                                                  
                                                  
                                                  

--Student Details:

Jack Danby - Phillips (7662823)
Sean Lacy - (100858144)

------------------Features/Bugs/Missing------------------

-Features-

- Truth Table Checking
- Forward Chaining
- Backward Chaining
- Opening .txt files with and without '.txt' extension added to initial arguements
- Horn form KB Only

-Bugs-

From the current implementation of all implemented features, there is no known bugs found by us through numerious testing of the application.

-Missing-
The only components missing in the application is the Research components with further expansion and development of the program.
Generic Knowledge Base Format is missing (only allowed for Horn-Form KB Input)

------------------Test cases------------------

-Reading text file Test Cases
	- Test if it saved the items/names appropiately without repeats
	- test if names read are valid/not according to the file
	- check if the specified item is being queried
	- check the various items relations

-Truth Table Check Test Cases
	- Test Output for given Example 1
	- Test Output for given Example 2
	- Test Output for Multi And Statement
	- Test Output for Non-Existant Query Within Rules
	- Test Output for Valid Rules but Invalid Query

-Bugs found and fixed through Test Case Implementation-
1. Test Output for Non-Existant Query Within Rules for Truth Table Check Caused the Program to crash when first implemented due to null exception, This has now been fixed.




------------------Acknowledgements/Resources------------------

For reading from the textfile, Truth Table Check and Forward Chaining and Backward Chaining method, all resources used were given throughout either lectures or tutorials. 
For these three components, the main Acknowledgements go towards Bao for helping us understand the main concepts behind these methods fully.
Once we grasped the understanding on all of these methods, we were able to easily implement the code to operate efficiently and effectively.
For the most part, the operation of this Assignment operated using similar methods as learned and explored through Assignment 1.
Such as reading from the text file and sorting the data in a efficient mannor and Utalising lists in a push and pop mannor to achieve effective and efficient methods.


------------------Notes------------------

The current implementation is just the basic, default implementation of the iengine criteria.
There is nothing special to note about our application besides the basic criteria that is stated in the given assignment 2 doccument.


------------------Summary report------------------

-Program Implementation-
Reading from textfile Initial Implementatiion(with single '&' Statement implementation) - Jack Danby - Phillips
Reading from textfile (allowing for multiple '&' Statements) - Sean Lacy
Forward Chaining - Jack Danby - Phillips
Backward Chaining - Sean Lacy
Truth Table Checking - Jack Danby - Phillips

-Unit Tests-
Reading From Text File - Jack Danby - Phillips
Truth Table Checking - Jack Danby - Phillips
Forward Chaining - Sean Lacy
Backward Chaining - Sean Lacy

-Team Member Feedback-
By Jack:
I believe throughout this assignment Sean worked throughly and efficiently throughout the project with great assistance in implementing the Reading from text file process
and through creating the backward chaining method working as intended.

By Sean:


-Overall Percentage Contribution-
Jack Danby - Phillips		50%
Sean Lacy			50%
-----------------------------------
Total				100%
