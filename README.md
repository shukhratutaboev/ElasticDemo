# ElasticDemo

This repository contains a sample setup for using Elasticsearch with .NET applications for logging. It includes a docker-compose file that spins up an Elasticsearch instance, Kibana for visualizing Elasticsearch data, and two .NET applications built with Dockerfiles for .NET 6 and .NET 5.

## Prerequisites

- docker and docker-compose installed on your system.
- A text editor for modifying the `docker-compose.yml` file.

## Usage

1. Clone this repository to your local machine: `git clone https://github.com/shukhratutaboev/ElasticDemo.git`
2. Navigate to the `ElasticDemo` directory: `cd ElasticDemo/src`
3. Open the `docker-compose.yml` file in a text editor and make any necessary changes.
   - The default Elasticsearch password is `strongpassword`. If you want to change the password, modify the `ELASTIC_PASSWORD` environment variable.
   - If you want to change the Elasticsearch and Kibana versions, modify the image tags in the `elasticsearch` and `kibana` services.
   - If you want to change the ports used by the .NET applications, modify the `ports` section for each of the `dotnet6` and `dotnet5` services.
4. Run the Docker Compose command to start the Elasticsearch, Kibana, and .NET applications: `docker-compose up -d`
5. Wait for Docker Compose to finish starting up the containers. This may take a few minutes.
6. Navigate to `http://localhost:9200` to confirm that Elasticsearch is up and running. You should see a JSON response with information about the Elasticsearch instance.
7. Navigate to `http://localhost:5601` to open Kibana. You should see the Kibana login screen.
8. Log in to Kibana with the default credentials (username: `elastic`, password: `strongpassword`).
9. In Kibana, create an index pattern to start visualizing Elasticsearch data.
10. Navigate to `http://localhost:8080/swagger/index.html` to access the .NET 6 application or `http://localhost:8081/swagger/index.html` to access the .NET 5 application.

## Clean up

To stop and remove the containers, run the following command in the `ElasticDemo/src` directory:

