# Release Notes

## [v1.0.0](https://github.com/me-stevens/unbeatable-nac/tree/v1.0.0)  2015-09-04

### What's new

The game can now be played in a 4x4 board as well, and the AI engine implements alpha–beta pruning.

[Compare with previous release](https://github.com/me-stevens/unbeatable-nac/compare/v0.0.0...v1.0.0)

### Changes

* Player class is now simplified.
* Class `AI` renamed to `AIMiniMax`. Now `AI` defines common methods for the other AI classes to inherit.
* The `IsWinner()` method in the `Board` class is now taken care of in the new `GameStats` class.
* The game stats in `Player` are also in the new class (since this is a zero-sum game, the wins of one player are the losses of the other and *vice versa*).
* Board constructor now calls `Reset()` to initialise its properties.

### Additions

* Adds a new `GameStats` class for the winner check and the winner stats.
* Adds new classes with different algorithms for the calculation of the best move: MiniMax, NegaMax, and their versions with alpha-beta branch-pruning.
* Adds instructions to setup the application and environment and how to run the tests.

### Removals

* Removes line calculation methods in the `Board` class.
* Removes the `FirstPos`, `LastPos`, and `placed` properties and `Reset()` method in the `Player` class.
* Removes the `ToString()` overrides in the `Board` and `Player` classes.
* Removes redundant/unnecessary comments.

### Fixes

* Pop up windows are now modal.
* Variables in all Test Units have proper informative names. Also, corrects naming according to WPF convention for `x:Key` and `x:Name` names.


## [v0.0.0](https://github.com/me-stevens/unbeatable-nac/tree/v0.0.0)  2015-08-21 

Hello world!
