Feature: Character Design
	In order to evaluate the character design of a warrior prince
	As a game developer
	I want to evaluate character behavior

Scenario Outline: Create a warrior
	Given Create a new <Warrior>
	Then Warrior Health must be <Health>
	And Warrior Magic must be <Magic>
Examples: 
	| Warrior | Health | Magic |
	| Ogre    | 100    | 80    |
	| Elf     | 80     | 100   |
