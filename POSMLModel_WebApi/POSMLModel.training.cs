﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace POSMLModel_WebApi
{
    public partial class POSMLModel
    {
        /// <summary>
        /// Retrains model using the pipeline generated as part of the training process. For more information on how to load data, see aka.ms/loaddata.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public static ITransformer RetrainPipeline(MLContext mlContext, IDataView trainData)
        {
            var pipeline = BuildPipeline(mlContext);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.Text.FeaturizeText(inputColumnName:@"Order_Name",outputColumnName:@"Order_Name")      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName:@"Customer_Name",outputColumnName:@"Customer_Name"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName:@"Product_Name",outputColumnName:@"Product_Name"))      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"Order_Name",@"Customer_Name",@"Product_Name"}))      
                                    .Append(mlContext.Transforms.NormalizeMinMax(@"Features", @"Features"))      
                                    .Append(mlContext.Regression.Trainers.FastTreeTweedie(new FastTreeTweedieTrainer.Options(){NumberOfLeaves=4,MinimumExampleCountPerLeaf=16,NumberOfTrees=15,MaximumBinCountPerFeature=103,FeatureFraction=0.998853725855634,LearningRate=0.140380801113535,LabelColumnName=@"Total",FeatureColumnName=@"Features"}));

            return pipeline;
        }
    }
}
