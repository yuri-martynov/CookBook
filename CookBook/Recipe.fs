module Recipe

open Functions

let get (getDishById: getDishById) dishId : Async<string> = async {
    let! dish = getDishById dishId
    return dish.recipe
}
