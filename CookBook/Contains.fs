module Contains

let get dishId (product: string) = async {
    let dish = Data.get dishId
    return dish.Ingredients.Products 
        |> Seq.exists (fun p -> p.Value = product )
}

