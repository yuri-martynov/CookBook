module Recipe

open Functions
open Types

let get (getDishById: getDishById) dishId : Async<string> = async {
    let! dish = getDishById dishId
    return 
        dish.recipe.steps
        |> Seq.map Format.step
        |> Format.list "<br/>"
        

}
