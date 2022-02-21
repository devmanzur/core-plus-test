# core-plus-test

Initial requirement breakdown:
In a real-world situation, this setup and decisions would heavily rely on what is the current setup our application is using. If we're already using something else for other reports it may not be wise to introduce a separate database for just this one. We would usually reuse an existing data store instead of introducing a new one unless the benefits are pretty big.

Since we will be doing a lot of queries and the volume of data we will be dealing with is pretty big, assuming we are using a relational database for appointments, we will use another NoSQL database for reports that are optimized for querying such a large database. SQL offers more comprehensive joining capabilities but since this report does not require anything like that I will be using Elastic search.

Why Elastic search? Based on my previous experience we had a requirement to implement this kind of reporting for our financial system with millions of transactions, after trying out different SQL and NoSQL databases we selected Elastic search as it provided superior performance and querying capabilities over others, we already had MongoDB and SQL Server in our existing setup but the performance improvement was so big we moved to Elastic search as our reporting database.

We transferred all our old data from SQL server to elastic search using logstash. For newer transactions, our Wallet Service sent asynchronous events to Reporting service. The wallet service continued to use SQL Server while the Reporting service was responsible for inserting data to elastic search based on the events it received.

So I will be using a similar setup for this as well where we would have a relational database as our primary database and Elastic search will have a copy of all the data optimized for querying and reporting.

Final notes:
After completing the application and comparing the results between Elastic search and SQL queries on the same dataset, the performance improvement is not enough to add Elastic search just for this report SQL performs well because there are not many joins for this report either. However, as per my previous experience, Elastic search bucket metrics provide superior performance over SQL even on Multilevel Nested objects with millions of records.
So although the difference is not evident in this case since we are speaking of Scalability our data will grow over time and Elastic search would be a good solution for all kinds of reporting, hence I have decided to stick with Elastic search for reporting.

## Built With
- C#, JavaScript
- .NET 6, VueJs, NUXTJS, and Tailwind CSS


### Prerequisites
- Docker or (.NET 6 and MSSQL Server database, Elastic search and npm)

## Running Application with docker on IDE
- Go to the src directory and run `docker-compose up -d`
- This will add all required dependencies to you docker container
- Go to ClientApplication folder inside CorePlus.Web and run npm install
- You can then run the application using your IDE without changing any settings

## Running Application on docker
- Go to the src directory and run `docker-compose build` then, `docker-compose up`
- The application should be available on http://localhost:8000

## Running Application in local environment using IDE
- Go to ClientApplication folder inside CorePlus.Web and run npm install
- Replace the CorePlusDb connection string with your own connection string for MSSQL database Or, You can use the current setting if you have sql server on docker locally
- Replace the ElasticConfiguration with your own Or, You can use the current setting if you have elastic search on docker locally
- Run the application

