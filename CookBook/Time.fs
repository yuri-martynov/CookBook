module Time

open Types
open Functions

let get (getDishById: getDishById) dishId : Async<string> = async {
    let! dish = getDishById dishId
    return dish.time |> Format.duration
}

