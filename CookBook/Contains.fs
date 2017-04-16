module Contains

let get dishId (product: string) = async {
    let dish = Data.get dishId
    let contains = 
        dish.Ingredients.Products 
        |> Seq.exists (fun p -> p.Value = product )

    if contains then
        return "Да"
    else
        return "Нет"
}

