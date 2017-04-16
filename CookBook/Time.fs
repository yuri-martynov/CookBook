module Time

open System
open Types
open Functions

let get (getDishById: getDishById) dishId : Async<string> = async {
    let! dish = getDishById dishId
    return dish.recipe.steps 
        |> Seq.map (fun s -> s.duration)
        |> Seq.fold (+) TimeSpan.Zero
        |> Format.duration
}

