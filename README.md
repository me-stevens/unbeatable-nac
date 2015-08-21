# Noughts and crosses

An implementation of the good ol' Nought and Crosses, to make an unbeatable boardgame using artificial intelligence. 

- The program allows the user to choose the game type (human v. computer, human v. human, or computer v. computer).  

- The computer player can win or draw, but never lose.

- The user has the choice of which player goes first.

For an overview of the code visit the dedicated site: https://me-stevens.github.io/noughts-and-crosses

## To do

- [ ] Extend to bigger boards. Implement depth control.
- [ ] Add more tests.
- [ ] Add animations? Or (responsibly) delay the bots to make them look like they are "thinking".
- [ ] Implement bindings. This is a bit tricky because the binding can only be made to one-dimensional arrays, which would need a restructuration of the `Board` class, and they should also implement the `INotifyPropertyChanged` interface, which would make the application non-GUI agnostic. It's also known to have [other problems](http://justinangel.net/automagicallyimplementinginotifypropertychanged).

## License

MIT License

## Dedicated site

https://me-stevens.github.io/noughts-and-crosses
