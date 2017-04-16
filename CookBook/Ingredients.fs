module Ingredients

open Types
open Functions

let get (getDishById: getDishById) id : Async<string> = 
    
    let mapProduct (product : Product) =
        product.name + " "  + product.quantity.ToString()
        

    async {
        let! dish = getDishById id
        return dish.ingredients 
            |> Seq.map mapProduct
            |> fun products -> System.String.Join("<br/>", products)
    }

