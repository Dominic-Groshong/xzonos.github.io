---
layout: notes
title: Cheat Sheet
permalink: /notes/
---

<h1 id="git">GIT</h1>
<hr>

##### Configure User Info
Configure user information for all local repositories

| Command | Description     |
| :------------- | :------------- |
| ```git config --global user.name "[name]"```    | Set the global username       |
| ```git config --global user.emial "[email address]"```   | Set the global email address |

##### Creating Repositories
Start a new repository or obtain existing repository

| Command | Description     |
| :------------- | :------------- |
| ```git init [project-name]```       | Create a new local repository        |
| ```git clone [url]```   | Clone the repository from url  |

##### Making Changes
Review edits and craft a commit

| Command | Description     |
| :------------- | :------------- |
| ```git status```       | List all new or modified files        |
| ```git diff```   | Shows file differences not yet staged  |
| ```git add [file]```   | Add the file use "." to add all files  |
| ```git diff --staged```   | Shows file differences between staging and the last file version  |
| ```git reset [file]```   | Unstages the file, but preserves it's contents  |
|	```git commit -m "[descriptive message]"```   | Records file snapshot and adds it in version history  |

##### Branches
Create and combine branches

| Command | Description     |
| :------------- | :------------- |
|	```git branch```   | List all local branches in current repository   |
| ```git branch [branch-name]```    | Create a new branch       |
| ```git checkout [branch-name]```   | Switches to the specified branch and updates working directory |
| ```git merge [branch]```  | Merges specified branch with current branch   |
| ```git branch -d [branch]```  | Deletes the specified branch  |
