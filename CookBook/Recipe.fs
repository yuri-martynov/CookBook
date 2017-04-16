module Recipe

open DataAccess

let private dataAccess = DishDataAccess()

let get (dish: string) : Async<string> = async {
    let str = dataAccess.Get dish
    let dish = DishXmlProvider.parse str
    return dish.Recipe.XElement.ToString()
}
