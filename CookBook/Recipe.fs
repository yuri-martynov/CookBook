module Recipe

let get (id: string) : Async<string> = async {
    let dish = Data.get id
    return dish.Recipe.XElement.Value
}
