demo app for the openfsharp.org talk on pursuing quality

Philosphy
API Testing
UI Automation
Performance Testing
Property Based Testing


###Philosophy
  Quality is a mindset,
    one person can't do it,
    you have to change culture (challenging)

###Why?
  Makes you learn about your app,
    ui tests help you understand from a user's standpoint,
    api from a consumer standpoint

###How? (Dev day to day)
  Build a second consumer of your api,
    another application

  Fresh data for every test is usually good,
    a new user for each one etc

  -1,0,1,2,N where N is a medium number and N * 10

###Cons
  Tests are a second consumer of your api,
    and can make refactoring take more time

  Quality can't be achieved without spending time pursuing it

  Learning curve

###Anecdotes of Wisdom

* All code is debt,
    make sure the code you are writing has a value that outweighs the debt

* "Everyone knows that debugging is twice as hard as writing a program in the first place.
    So if you're as clever as you can be when you write it,
    how will you ever debug it?" - Brian Kernighan

* Code that 'should work' usually works < 50% of the time

* What you measure often defines your results
    If developers are measured on getting tickets to 'Ready to QA' then that will influence their quality
    Try measuring for low production bugs with good velocity

* You test scenarios,
    and scenarios rely on data,
    and test data should be easy to create,
    otherwise people wont test those scenarios!

* Generally two problems to solve, once how to do solve it in general,
    and then how to solve it within the constraints of your application

* Breaking happy path is embarrassing!

  Don’t just 'run it again'

  Slow your api calls down to find UI bugs

  Test Data is often a bottleneck for development and testing, automate it!!!

  Things that are easy to reproduce are easier to fix

  POC POC POCs help so much

  TDD's value is over stated, so is code coverage
    I think its ok to say you coverage is too low
    but there isn't a number that is 'high enough'
    use your head and make sure you have high value things covered

  Bike parts are stress tested and have known limits
    Your app should too?

  Many times a bug is an indication of a larger architectural problem
    (things like db locking, or perfomance problems,
     not things like > instead of >=)

  Testing adds value mainly because you spend time trying to break something,
    and partly because of the artifacts

  Property base testing will proactively find bugs
    Steep learning curve
    Runs fast like unit tests

  API Integration tests will find bugs AND prevent regressions
    Contract validation can be useful, especially around regressions and lack of cross team communications

  UI automation will find many bugs AND prevent regressions
    Its OK to create live mocks for scenarios against third party vendors like a CC processor

  Performance is a feature and if your application is not reasonably performant it’s a bug
    Be aware and mindful early on because its easier to catch regression and fix them before they go live
      than it is to refactor or redesign them away once you are live
    What is fast? Define it and try to hit your goals

  Big difference between what a framework can do,
    and what people can make it do (especially reasonable performance)

  Set a goal for time limit on your tests.
    I like 10 minutes or less.
    If you go above maybe make a subset of your tests that run fast,
    and a full run that takes longer

  Where to start?
  Find out what is keeping people from doing quality related tasks
    Clear those roadblocks

  AVOID SLEEPS
