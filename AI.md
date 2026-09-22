which tools i used and what for?

any incorrect/unhelpful suggestions

how i verified it works

used ai for:
- initial set up wriyting hello world
- turning code into a fucntion and calling from program.cs

asked to make it capitalised it suggested .ToUpper() but that turned everythign capital. solution it gave to make just firts letter capital was too convoluted so i ignored
-ask ai to help with bot logic. gettign1valid move grid list

ai help for:
- input grid roatate all 4, check eveyr column, see if a move is valid, if so return a list of new grid with all valid inputs. the generated coce was too big to fully understand, so had to break down into smaller steps
.
- make the grid object skeleton. i will edit myself and add functiosn as needed

learnty looping through a toople

learnt continue cant be used in ifs, only loops

'Grid' does not contain a definition for 'Clone' and no accessible extension method 'Clone' accepting a first argument of type 'Grid' could be found (are you missing a using directive or an assembly reference?) - problem is clien method isnt built in we had to make one

learnt to add 

big problem. function A doesnt work propertly becausr it relies on fucntion B. if i change function B then functiomns C, D, E stop working. the original implementaiton was buggy. need to make a dpublicate function of B thats slightly differnt to the original so A can use it
leanr synatx for many things. kknew logic


Added check win conditonm 4 in a roaw
How to check if input is string or int

add gitinore (what files ti ignore and remove previously tracked junk files)

add logic to rotate right and left

leaqrnt static class vs non static. chose to create a new player class (nomn static) so i dont wont have to re format the already existign static class. woudl be more efficient if i did if i had more time
learnt get ; private set is needed to make changes to variable

Testing:
blank names. loop until name is not blank

adding unit tests - only a generic sample. testign 2+3 = 6, not showing as "incorrect". unhelpful sugegstion from ai. fixed with "dotnet new xunit" command

create playersetup tests to test differe4nt edge cases of getting thr name. i only need the synatx. i can add the test cases myself

couldnt refernece main file to test

public static method must be inside class first to work