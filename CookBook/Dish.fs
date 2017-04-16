module Dish

open Types
open Functions

let findByIngredients (getDishesByIngredients : getDishesByIngredients) (getDishById: getDishById) (products: seq<string>): Async<string> = async {
    let! dishIds = getDishesByIngredients products
    let id = dishIds |> Seq.tryHead
    match id with
    |Some id -> return! Ingredients.get getDishById id
    |None -> return "нет("
}
    

