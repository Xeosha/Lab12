using InputKeyboard;
using Menu;
using Lab_10lib;
using BinarySearchTree;
using Lab12;
using System.Collections;
using System.Security.Cryptography;

namespace Xeosha
{
    public class Program
    {
        public static Goods[] GenerateRandomGoodsArray(int size)
        {
            Goods[] products = new Goods[size];
            var rnd = new Random();

            for (int i = 0; i < size; i++)
            {
                int item = rnd.Next(1, 5);
                products[i] = CreateProduct(item);
            }

            return products;
        }

        public static Goods CreateProduct(int itemType)
        {
            return itemType switch
            {
                1 => new Goods(),
                2 => new Toy(),
                3 => new Product(),
                4 => new MilkProduct(),
                _ => throw new ArgumentException("Неизвестный тип!"),
            };
        }

        static Goods[] CreateRandomGoodsArray()
        {
            int size = EnterKeybord.TypeInteger("Введите размер массива: ", 0);
            return GenerateRandomGoodsArray(size);
        }



        static Goods[] CreateKeyboardGoodsArray()
        {
            int size = EnterKeybord.TypeInteger("Введите размер массива: ", 0);
            Goods[] products = new Goods[size];
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"\n\t>> Создание {i} товара: ");
                products[i] = new Goods();
                products[i].Init();
            }

            return products;
        }
        static Goods[] CreateArray()
        {
            Goods[] goods = Array.Empty<Goods>();

            var dialog = new Dialog("Создание массива товаров");
            dialog.AddOption("Рандомное создание", () => { goods = CreateRandomGoodsArray(); dialog.Close(GetStringProducts(goods)); });
            dialog.AddOption("Создание с клавиатуры", () => { goods = CreateKeyboardGoodsArray(); dialog.Close(GetStringProducts(goods)); }, true);
            dialog.Start();

            return goods;
        }

        public static string GetStringProducts(Goods[] products, string message = "Товары в массиве:")
        {
            var result = "\t" + message + "\n";
            foreach (Goods product in products)
                result += product + "\n";
            return result;

        }      

        public static void AddProductInTree(BinaryTree<Goods> tree)
        {
            var products = CreateArray();

            tree.AddRange(products);
        }


        public static void TaskClone(BinaryTree<Goods> tree)
        {
            if (tree.Count == 0)
            {
                var products = GenerateRandomGoodsArray(5);
                tree = new BinaryTree<Goods>(products, new CustomComparer());
            }
            tree.Add(new Goods("1", 1, 1));


            Console.WriteLine("\n>>\tИзначальное дерево: ");
            tree.PrintTree();

            Console.WriteLine("\n>>\tПоверхностное копирование дерева: ");
            var treeCLone = ((BinaryTree<Goods>)tree.ShallowCopy());
            treeCLone.PrintTree();
             
            Console.WriteLine("\n>>\tГлубокое копирование дерева: ");
            var treeShallow = ((BinaryTree<Goods>)tree.Clone());
            treeShallow.PrintTree();

            Console.WriteLine("Удаление обьекта 1 1 1");
            tree.Remove(new Goods("1", 1, 1));

            Console.WriteLine("\n>>\tПоверхностное копирование дерева: ");
            treeCLone.PrintTree();

            Console.WriteLine("\n>>\tГлубокое копирование дерева: ");
            treeShallow.PrintTree();
        }

        static void ShowTree(BinaryTree<Goods> tree)
        {
            Console.WriteLine("\tКоличество товаров в дереве поиска: " + tree.Count);

            Console.WriteLine();

            Console.WriteLine("\tДерево поиска через PrintTree:");
            tree.PrintTree();

            Console.WriteLine();

            Console.WriteLine("\tIEnumerable:");
            foreach (var item in tree)
                Console.WriteLine(item);
        }

        static void DeleteItemTree(BinaryTree<Goods> tree)
        {
            foreach (var item in CreateArray())
                tree.Remove(item);

        }

        static void FindItem(BinaryTree<Goods> tree)
        {
            var product = new Goods();
            product.Init();

            var findProduct = tree.Find(product);
            Console.Clear();
            Console.WriteLine("\tНайденный продукт: ");
            if (findProduct != null)
                Console.WriteLine(findProduct);
            else
                Console.WriteLine("Товар не найден");
            //uiii
        }

        //static void fun()
        //{
        //    var tree = new BinaryTree<CloneableInt>(new List<CloneableInt>{9, 5, 4, 8, 6, 7 });
        //    tree.PrintTree();
        //    tree.Remove(5);
        //    tree.PrintTree(); 

        //    Console.ReadKey(true);
        //}

        public static void Main()
        {
            //fun();
            // Компаратор по цене.
            var searchTree = new BinaryTree<Goods>(new CustomComparer());

            var dialog = new Dialog("12-ая Лабораторная работа");
            dialog.AddOption("Добавление нового элемента (ов) ", () => AddProductInTree(searchTree), true);
            dialog.AddOption("Удаление элемента (ов) ", () => DeleteItemTree(searchTree), true);
            dialog.AddOption("Вывод дерева (+ IEnumerable)", () => ShowTree(searchTree));
            dialog.AddOption("Поиск элемента", () => FindItem(searchTree));
            dialog.AddOption("Удалить дерево из памяти", () => { searchTree.Clear(); });
            dialog.AddOption("Клон дерева", () => TaskClone(searchTree));
            dialog.Start();


        }

    }
}