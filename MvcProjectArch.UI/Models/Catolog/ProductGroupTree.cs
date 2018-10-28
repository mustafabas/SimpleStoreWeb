using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcProjectArch.UI.Models.Catolog
{
    public class ProductGroupTree
    {
        ProductGroupTreeNode root;
        public List<ProductGroupTreeNode> ProductNode { get; set; }
        public List<ProductItemModel> AllProductByCategoryName { get; set; }
        public List<ProductItemModel> GetAllProduct { get; set; }
        public List<ProductItemModel> ProductsBetween { get; set; }
        public int[] elementCountForEachDepth;
        public string[] sumElementCountForEachDepth;
        public int maxDepth;
        public int totalDepth;

        //public ProductHeap ProductHeap { get; set; }
        public int TreeSize;
        public int ProductTotalSize;
        public ProductGroupTree()
        {
            //ProductHeap = new ProductHeap(TreeSize);
            this.ProductNode = new List<ProductGroupTreeNode>();
            this.AllProductByCategoryName = new List<ProductItemModel>();
            this.GetAllProduct = new List<ProductItemModel>();
            this.ProductsBetween = new List<ProductItemModel>();
        }

        public ProductGroupTreeNode GetRoot()
        {
            return root;
        }
        public void inOrder(ProductGroupTreeNode root)
        {
            if(root!=null)
            { 
            inOrder(root.LeftChild);
            ProductNode.Add(root);
           
            inOrder(root.RightChid);
            }
        }
        public void postOrder(ProductGroupTreeNode root)
        {
            if (root != null)
            {
                postOrder(root.LeftChild);
                postOrder(root.RightChid);
                ProductNode.Add(root);
            }
        }
        public void preOrder(ProductGroupTreeNode root)
        {
            if (root != null)
            {
                ProductNode.Add(root);
                preOrder(root.LeftChild);
                preOrder(root.RightChid);
               
            }
        }
        public void inOrderForProductsAllProduct(ProductGroupTreeNode root)
        {
            if (root != null)
            {
                
                foreach (var item in root.ProductBase.Products.ToList())
                {
                    if(item.ProductNumber>0)
                    { 
                         AllProductByCategoryName.Add(item);
                    }
                }
                ProductTotalSize = ProductTotalSize + root.ProductBase.Products.Count;
                inOrderForProductsAllProduct(root.LeftChild);
                inOrderForProductsAllProduct(root.RightChid);
            }

        }
        public void GetAllProductByProductGroupName(ProductGroupTreeNode root, string productGroupName)
        {
            if (root != null)
            {
                if (root.ProductBase.ProductGroupName == productGroupName)
                {
                    GetAllProduct.AddRange(root.ProductBase.Products);
                }
                else
                {
                    GetAllProductByProductGroupName(root.LeftChild, productGroupName);
                    GetAllProductByProductGroupName(root.RightChid, productGroupName);
                }
            }
        }
        public void GetAllProductPriceBetween(ProductGroupTreeNode root, decimal productPriceBegin,decimal productPriceLast)
        {
            if (root != null)
            {
                foreach (var item in root.ProductBase.Products)
                {
                    if (item.ProductPrice >= productPriceBegin && item.ProductPrice <= productPriceLast)
                    {
                        ProductsBetween.Add(item);
                    }
                }
                GetAllProductPriceBetween(root.LeftChild, productPriceBegin, productPriceLast);
                GetAllProductPriceBetween(root.RightChid, productPriceBegin, productPriceLast);
                
            }
        }
        public void DeleteProducts(ProductGroupTreeNode root,List<int> productIds)
        {
            if(root!=null)
            {
                foreach (var item in productIds)
                {
                    var product = root.ProductBase.Products.Where(x => x.ProductId == item).FirstOrDefault();
                    if (product != null)
                        product.ProductNumber = product.ProductNumber - 1;
                }
            
                DeleteProducts(root.LeftChild, productIds);
                DeleteProducts(root.RightChid,productIds);
            }
            
        }

        public void insert(ProductGroup newProductBase)
        {
            ProductGroupTreeNode newNode = new ProductGroupTreeNode();
            newNode.ProductBase = newProductBase;
            TreeSize++;
            if (root == null)
                root = newNode;
            else
            {
                ProductGroupTreeNode current = root;
                ProductGroupTreeNode parent;
                while(true)
                {
                    parent = current;
                    if (newNode.ProductBase.ProductGroupName.CompareTo(current.ProductBase.ProductGroupName) == 0)
                    {
                        current = current.LeftChild;
                        if (current == null)
                        {
                            parent.LeftChild = newNode;
                        return;
                        }
                    }
                    else
                    {
                        current = current.RightChid;
                        if(current==null)
                        {
                            parent.RightChid = newNode;
                            return;
                        }
                    }

                }
            }
        }
        
        public ProductGroupTreeNode GetProductTreeNodeByProductGroupName(ProductGroupTreeNode root,string productGroupName)
        {
            if(root!=null)
            {
                if(root.ProductBase.ProductGroupName==productGroupName)
                {
                    return root;
                }
                    if(root.ProductBase.ProductGroupName.CompareTo(productGroupName)==0)
                        return GetProductTreeNodeByProductGroupName(root.LeftChild, productGroupName);
                    else
                       return GetProductTreeNodeByProductGroupName(root.RightChid, productGroupName);
            }
            else
            {
                return new ProductGroupTreeNode();
            }
        }
        public void findTreeInfo(ProductGroupTreeNode rootNode, int treeSize)
        {

            totalDepth = 0;
            maxDepth = 0;

            elementCountForEachDepth = new int[treeSize];
            sumElementCountForEachDepth = new string[treeSize];

            traverseTreeForInfo(rootNode, -1);

            //Console.WriteLine("\nDepth of the tree: " + maxDepth);
            //Console.WriteLine("Element counts for each depth");
            //for (int i = 0; i <= maxDepth; i++)
            //{
            //    Console.WriteLine("\tFor depth {0}: Number of elements: {1}  Sum of elements {2}", i, elementCountForEachDepth[i], sumElementCountForEachDepth[i]);
            //}
            //Console.WriteLine("Average depth: " + ((double)totalDepth / treeSize));

        }

        private void traverseTreeForInfo(ProductGroupTreeNode node, int depth)
        {
            if (node != null)
            {
                depth++;

                elementCountForEachDepth[depth]++;
                sumElementCountForEachDepth[depth] += node.ProductBase.ProductGroupName;

                if (depth > maxDepth)
                    maxDepth = depth; //update max depth

                totalDepth += depth;

                traverseTreeForInfo(node.LeftChild, depth); //traverse left sub-tree
                traverseTreeForInfo(node.RightChid, depth); //traverse right sub-tree

            }
        }
        public void düzeyListele(ProductGroupTreeNode etkin, int d)
        {
            if (etkin != null)
            {
                d = d + 1;
                düzeyListele(etkin.LeftChild, d);
                //Console.WriteLine(" " + etkin.ProductBase.ProductGroupName + " " + d + ". düzeyde");
                düzeyListele(etkin.RightChid, d);
            }
        }


    }
}