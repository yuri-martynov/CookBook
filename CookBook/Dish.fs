module Dish

open Types

let findByIngredients 
    (getDishesByIngredients : getDishesByIngredients) 
    (getDishById: getDishById) (products: seq<string>)
    : Async<string> = 
    async {
        let! dishIds = getDishesByIngredients products
        let id = dishIds |> Seq.tryHead
        match id with
        |Some id -> 
            let! dish = getDishById id
            return dish.name
        |None -> return "нет("
    }
    

