module Ingredients

open Types
open Functions
open Format

let get (getDishById: getDishById) id : Async<string> = 
    
    async {
        let! dish = getDishById id
        return dish.ingredients 
            |> Seq.map format
            |> fun products -> System.String.Join("<br/>", products)
    }

