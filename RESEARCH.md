My first and biggest issue was not knowing **C#**. I used **W3Schools** and GitHub Copilot to help me understand the correct syntax. Since I was familiar with Java, translating into C# was not very difficult

Other than this, I have written down a list of specific problems I encountered:

**String manipulation**
- I wanted to capitalise the input names, so searched Microsoft Learn & W3Schools
- They held generic information not relevant to what I wanted
- StackOverflow suggested an answer, but it required splitting the string, capitalising the first character and re-joining
- I decided this method was not worth the complexity for this requirement

**Tuple data types**
- Many functions required returning a Tuple of multiple data types
- In general, I struggle understanding how Tuples work, so I searched what I am familiar with first
- W3Schools teaches Tuples with Python, which explained the basics of what they are
- Since Python is not strict on data types, this did not help fully as my C# tuples needed to have specific data
- I used the same python logic to attempt the Tuple data type, and it worked

**Static vs non-static**
- I had learnt this whilst studying Java, but did not recall initially
- I originally made methods/classes that were a mixture of static & non-static
- After they failed, I switched some around and could not understand why it worked
- I then recalled that static methods can be called without instantiating the class
- Some of the older classes/functions may still have the wrong implementation, but still work

**Nested methods**
- Especially whilst creating the Robot logic, many functions relied on another
- When one was faulty, changing the root function would cause other ones to fail
- I often times had to change the entire signature of a function to make it work, then change all instances of where it was called and how the return data is handled

**Junk files**
- Initially, every small change I made would create dozens of junk files which were committed into GitHub
- I recalled the use of .gitignore for these files
- I found a .gitignore template for C# on GitHub, then removed all previously tracked junk files

**get; private set**
- I originally learnt this concept during OOP studies for Python, but forgot
- Working with C# and creating Objects reminded me how the public, private, and protected methods work

**Unit tests**
- Conceptually I understand them, such as initial set ups, creating mocks, assertions, clean ups etc
- My biggest issue was connecting my C# syntax Unit test to the actual code itself
- I created a simplified test and used "dotnet new xunit" to start, suggested by Copilot
