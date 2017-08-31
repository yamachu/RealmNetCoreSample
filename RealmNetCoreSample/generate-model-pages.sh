#!/bin/bash

model_name=$1
dotnet aspnet-codegenerator --no-build razorpage -m ${model_name} -dc DummyDatabaseContext -outDir Pages/Admin/${model_name}
