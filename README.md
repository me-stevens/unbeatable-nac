# Noughts and crosses

An implementation of the good ol' Nought and Crosses, to make an unbeatable boardgame using artificial intelligence. 

- The program allows the user to choose the game type (human v. computer, human v. human, or computer v. computer).  

- The computer player can win or draw, but never lose.

- The user has the choice of which player goes first.

For an overview of the code visit the dedicated site: http://me-stevens.github.io/unbeatable-nac

## To run the application

This is a WPF (*Windows Presentation Foundation*) application for Windows. To run it, you need Visual Studio 2012 or superior. You can download it for free [here](https://www.visualstudio.com/en-us/visual-studio-homepage-vs.aspx).

Double click on the "solution" file (`NoughtsAndCrosses.sln`) to open the application in VS, then hit the green **Play** button to run it. This will open the `MainWindow.xaml.cs` window. 

Click **New Game** and have fun!

## To run the tests

In Visual Studio, go to **TEST > Windows > Test Explorer** to open the Test Explorer panel. Then, in said panel, click **Run All**.


## To do

- [ ] Implement move-ordering? Check Dictionary or HashMap classes out.
- [ ] Find a good code profiler.
- [ ] Implement the MVVM pattern for the views.
- [ ] Implement bindings. This is a bit tricky because the binding can only be made to one-dimensional arrays, which would need a restructuration of the `Board` class, and they should also implement the `INotifyPropertyChanged` interface, which would make the application non-GUI agnostic.

## Dedicated site

http://me-stevens.github.io/unbeatable-nac


## License

[![License](https://img.shields.io/badge/gnu-license-green.svg?style=flat)](https://opensource.org/licenses/GPL-2.0)
GNU License
