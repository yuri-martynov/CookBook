module Time

open System
open Types

let get' (dish : Dish) : TimeSpan =
    dish.recipe.steps 
    |> Seq.map (fun s -> s.duration)
    |> Seq.fold (+) TimeSpan.Zero

let get (getDishById: getDishById) dishId : Async<string> = async {
    let! dish = getDishById dishId
    let duration = get' dish
    return duration |> Format.duration
}




