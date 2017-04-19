module Ingredients

open Types

let get (getDishById: getDishById) id : Async<string> = 
    
    async {
        let! dish = getDishById id
        return dish
            |> Utils.ingredients 
            |> Seq.map Format.ingredient
            |> Format.list ". "
    }

