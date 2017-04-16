module Recipe

open DataAccess

let private dataAccess = DishDataAccess()

let get (dish: string) : Async<string> = async {
    let dish = dataAccess.Get dish
    return dish.Recipe
}
