# Overview - Chlorophyll

This project was made as a final project for my Games & Society class, created with the help of Elena Kosowski and Ava DiPietro. The objective of the project was to create a concept for a game that provided commentary on some social issue. We chose to address the mental health issues students face at schools and universities, a topic that was particularly relevant given the online learning situations we all found ourselves in due to the pandemic.

An actual game, rather than a concept, was not required for the final project. However, we chose to create at least a working prototype of the game. We worked over a course of 3-4 weeks. All of the scripts and Unity objects in this reposity were developed by myself. 

## Concept

In Chlorophyll, you are a student working at your desk room. At another desk is a potted plant, which is meant to represent the state of your health.  As time passes, the plant will gradually worsen in health, and you must maintain it by watering it whenever possible. Your objective is to keep the plant alive for as long as possible, while performing other tasks that keep the plant healthy:

## Mechanics

You receive assignments as the day passes, and these assignments must be completed before the time at which they are due. Letting assignments pile up without completing them will accelerate your plant's wilting, as will having overdue assignments. Additionally, making mistakes on assignments will impact your grade, and having a lower grade will also worsen your plant's health.

The in-game time is divided up into days. At the start of the day, your plant will remain fairly healthy, and only a few easier assignments will be sent to you. The further you get into the day, the quicker your plant will wilt, and the assignments you receive will also become more difficult and greater in number. This process repeats each new day, and each day the overall magnitude of all of these factors increases.

The only assignment that appears in this prototype is a math assignment, in which you must complete an assortment of [Lights out!](https://en.wikipedia.org/wiki/Lights_Out_(game)) puzzles, with the objective of making all of the blue tiles red. An assignment can be submitted in any state. However, if it is only partially complete - in this case, if some blue squares remain - you will receive a less than perfect score on the assignment, which will impact your grade and, by extension, your plant's health. On the first day, there are initially only two three-tile puzzles and one four-tile puzzle in each assignment. As the game progresses, the number of tiles in each puzzle will increase, and eventually there will also be four puzzles instead of three. This maxes out at four twenty-five-tile puzzles.

The game ends if your plant's health bottoms out, represented by a grey, wilted plant.

# Code Design & Implementation

## Events

This is the first project in which I learned to use events to reduce dependencies. Most of the game was dependant on a timer that ticked every second, using the value of the timer (the number of seconds in the day, and the day) to advance the game. Additionally, many aspects of the game were further dependant on the assignments that were both created and submitted, used to change the player's grade or add to the list of assignments that the player can view.

Instead of passing this information around through references and strict dependencies on other objects, every script simply listens to events broadcast by a singular EventManager. This means that if I wanted to edit or replace certain features that broadcast events to other scripts, like the timer or the assignment creation script, I would only have to modify how those events are broadcasted. How an object actually receives that event remains completely unmodified, which saves a lot of work and confusion.

Adding an event is also fairly simple. I simply had to add the event to the EventManager class, then add a method that broadcasts the event if anything is listening to it.

It was not relevant in this project, but I later learned that an object must explicitly be instructed to stop listening to events once its GameObject is destroyed.

### Improvements

The lone EventManager held a lot of events, and the script became fairly messy. Additionally, in order to reduce the amount of work needed to allow scripts to subscribe or unsubscribe from events, I simply gave every script direct access to the events, allowing them to also invoke the event, which is not intentional. In the future, I could find a better design that further decouples each script from the EventManager, and that properly protects the events themselves.

