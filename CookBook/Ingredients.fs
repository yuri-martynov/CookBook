module Ingredients

open Types
open Functions

let get (getDishById: getDishById) id : Async<string> = 
    
    async {
        let! dish = getDishById id
        return dish.ingredients 
            |> Seq.map Format.ingredient
            |> Format.list "<br/>"
    }

