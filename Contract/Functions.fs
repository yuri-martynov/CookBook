module Functions

open Types

type getDishById = string -> Async<Dish>

type getDishesByIngredients = seq<string> -> Async<seq<string>>