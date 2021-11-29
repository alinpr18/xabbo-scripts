# xabbo scripts

A collection of scripts for use with the [xabbo scripter](https://github.com/b7c/Xabbo.Scripter).

# contributing

Do not share scripts that are malicious or aimed at disrupting the game.

You may use the following metadata at the top of your scripts.\
Please include the version of the scripter it was written for with the `@scripter` property.\
Currently only `@name` and `@group` are supported by the scripter.

```cs
/// @name Script name
/// @group Group name
/// @desc Description here.
/// @author Author name
/// @scripter 1.0.0-beta
```

If your script requires additional files, place the script in its own folder along with the additional files in its own `io` folder.
