using RestaurantApi.Models.DishModels;

namespace RestaurantApi.Models.DataContext
{
    public static class SeedData
    {
        private const string ImagePath = "Models/DataContext/SeedPhotos";

        public static void SeedDatabase(DataContext context)
        {
            if (context.Dishes.Count() == 0
                && context.Images.Count() == 0
                && context.DishTypes.Count() == 0)
            {
                DishType burger = new() { Name = "Burger" };
                DishType pizza = new() { Name = "Pizza" };
                DishType salad = new() { Name = "Salad" };
                DishType soup = new() { Name = "Soup" };                

                Dish burger1 = new()
                {
                    Name = "First burger",
                    Description = "Indulge in our delectable burgers crafted with love and precision. A tender patty, fresh veggies, and soft buns – each element of our burger is chosen with meticulous attention to detail. Explore a variety of juicy flavor combinations, from classic hamburgers to unique signature creations. Experience a genuine explosion of taste in every bite! Visit us and craft your own unforgettable gastronomic adventure with our burgers. Embrace true delight – in every bite, in every moment",
                    Price = 45m,
                    DishType = burger,
                    Image = new()
                    {
                        BitMap = File.ReadAllBytes($"{ImagePath}/burger-1.jpg"),
                        ContentType = "image/jpeg"
                    }
                };

                Dish burger2 = new()
                {
                    Name = "Second burger",
                    Description = "Indulge in our delectable burgers crafted with love and precision. A tender patty, fresh veggies, and soft buns – each element of our burger is chosen with meticulous attention to detail. Explore a variety of juicy flavor combinations, from classic hamburgers to unique signature creations. Experience a genuine explosion of taste in every bite! Visit us and craft your own unforgettable gastronomic adventure with our burgers. Embrace true delight – in every bite, in every moment",
                    Price = 55m,
                    DishType = burger,
                    Image = new()
                    {
                        BitMap = File.ReadAllBytes($"{ImagePath}/burger-2.jpg"),
                        ContentType = "image/jpeg"
                    }
                };

                Dish pizza1 = new()
                {
                    Name = "First pizza",
                    Description = "Delight in our exquisite pizzas, meticulously crafted with passion. From premium cheeses to fresh vegetables and savory meats, each ingredient is carefully chosen. Explore a variety of flavors, from timeless classics to exclusive creations. Immerse yourself in an authentic burst of taste with every bite! Join us for an unforgettable gastronomic journey. Savor true delight – in every slice, in every moment.",
                    Price = 70m,
                    DishType = pizza,
                    Image = new()
                    {
                        BitMap = File.ReadAllBytes($"{ImagePath}/pizza-1.jpg"),
                        ContentType = "image/jpeg"
                    }
                };

                Dish pizza2 = new()
                {
                    Name = "Second pizza",
                    Description = "Delight in our exquisite pizzas, meticulously crafted with passion. From premium cheeses to fresh vegetables and savory meats, each ingredient is carefully chosen. Explore a variety of flavors, from timeless classics to exclusive creations. Immerse yourself in an authentic burst of taste with every bite! Join us for an unforgettable gastronomic journey. Savor true delight – in every slice, in every moment.",
                    Price = 65m,
                    DishType = pizza,
                    Image = new()
                    {
                        BitMap = File.ReadAllBytes($"{ImagePath}/pizza-2.jpg"),
                        ContentType = "image/jpeg"
                    }
                };

                Dish salad1 = new()
                {
                    Name = "First salad",
                    Description = "Experience the freshness of our vibrant salads, crafted with care and dedication. Each salad is a symphony of crisp greens, garden-fresh vegetables, and delectable dressings. From classic combinations to unique, chef-inspired creations, we offer a variety of flavors to suit every palate. Dive into a world of wholesome taste with every forkful! Join us to create your own culinary adventure with our salads. Revel in true freshness – in every bite, in every moment.",
                    Price = 35m,
                    DishType = salad,
                    Image = new()
                    {
                        BitMap = File.ReadAllBytes($"{ImagePath}/salad-1.jpg"),
                        ContentType = "image/jpeg"
                    }
                };

                Dish salad2 = new()
                {
                    Name = "Second salad",
                    Description = "Experience the freshness of our vibrant salads, crafted with care and dedication. Each salad is a symphony of crisp greens, garden-fresh vegetables, and delectable dressings. From classic combinations to unique, chef-inspired creations, we offer a variety of flavors to suit every palate. Dive into a world of wholesome taste with every forkful! Join us to create your own culinary adventure with our salads. Revel in true freshness – in every bite, in every moment.",
                    Price = 40m,
                    DishType = salad,
                    Image = new()
                    {
                        BitMap = File.ReadAllBytes($"{ImagePath}/salad-2.jpg"),
                        ContentType = "image/jpeg"
                    }
                };

                Dish soup1 = new()
                {
                    Name = "Second soup",
                    Description = "Delve into the comforting warmth of our hearty soups, carefully prepared with love and expertise. Each spoonful is a journey through rich flavors, wholesome ingredients, and meticulous seasoning. From timeless classics to inventive creations, our soup offerings cater to every taste. Experience a bowlful of nourishing goodness with every sip! Join us for a culinary exploration with our soul-warming soups. Embrace true comfort – in every spoonful, in every moment.",
                    Price = 50m,
                    DishType = soup,
                    Image = new()
                    {
                        BitMap = File.ReadAllBytes($"{ImagePath}/soup-1.jpg"),
                        ContentType = "image/jpeg"
                    }
                };

                Dish soup2 = new()
                {
                    Name = "Second soup",
                    Description = "Delve into the comforting warmth of our hearty soups, carefully prepared with love and expertise. Each spoonful is a journey through rich flavors, wholesome ingredients, and meticulous seasoning. From timeless classics to inventive creations, our soup offerings cater to every taste. Experience a bowlful of nourishing goodness with every sip! Join us for a culinary exploration with our soul-warming soups. Embrace true comfort – in every spoonful, in every moment.",
                    Price = 45m,
                    DishType = soup,
                    Image = new()
                    {
                        BitMap = File.ReadAllBytes($"{ImagePath}/soup-2.jpg"),
                        ContentType = "image/jpeg"
                    }
                };

                context.Dishes.AddRange(
                    burger1,
                    burger2,
                    pizza1,
                    pizza2,
                    salad1,
                    salad2,
                    soup1,
                    soup2
                );

                context.SaveChanges();
            }
        }
    }
}
