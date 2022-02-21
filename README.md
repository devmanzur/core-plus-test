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
