# Docker hub image - abhinabsarkar/abs-aspnetapi
A sample docker app built on Alpine linux 3.1 version using C# dotnet core version 3.1. This demo API returns details of the server on which it runs, including the host name & ip address. It also returns the time when the API was invoked.

The docker image can be downloaded from docker hub by running the below command 
```bash
docker pull abhinabsarkar/abs-aspnetapi:<version>
```
Size of the docker image is 114 MB.

The application can be run on any kubernetes cluster. A sample deployment.yaml can be found under the kubernetes folder. 

### To build the image, run docker build from the root directory of the application
```bash
# Build the abs-aspnetapi image
docker build -t abs-aspnetapi:v1.0.0 .
# Run the docker container locally
# The release version runs the container at port 80 although the app is running at port 5000
docker run --name abs-aspnetapi-container -d -p 8002:80 abs-aspnetapi:v1.0.0
# Check the status of the container
docker ps -a | findstr abs-aspnetapi-container
# Test the app
curl http://localhost:8002/api/absdemo
# log into the running container 
docker exec -it abs-aspnetapi-container /bin/bash
docker exec -it abs-aspnetapi-container <command>
# Remove the container
docker rm abs-aspnetapi-container -f
# Remove the image
docker rmi abs-aspnetapi:v1.0.0
# Push the image to docker hub
docker login
# Tag the local image & map it to the docker repo
docker tag local-image:tagname new-repo:tagname
# eg: docker tag abs-aspnetapi:v1.0.0 abhinabsarkar/abs-aspnetapi:v1.0.0
# push the tagged image to the docker hub
docker push new-repo:tagname
# eg: docker push abhinabsarkar/abs-aspnetapi:v1.0.0
```

### To run the application on any Kubernetes platform (including Docker-Desktop) with ConfigMap
```bash
# Create namespace
kubectl create namespace abs-api
# Run the deployment config. A sample deployment config is placed under the kubernetes folder 
kubectl apply -f abs-aspnetapi.yaml -n abs-api
# Check the config map
kubectl describe configmap/aspnetapi -n abs-api
# Check if the application pod is running 
kubectl get all -n abs-api -o wide
# Peek into the running pod in k8s for debugging 
kubectl  -n <namespace> exec -it <pod-name> -- /bin/bash
kubectl  -n abs-api exec -it aspnetapi-548d449d5-h9zz7 -- /bin/bash
kubectl -n <namespace> exec --stdin --tty <pod-name> -- /bin/bash
# Delete the namespace & all the resources
kubectl delete namespace abs-api
```

### To expose the application on NodePort in Docker-Desktop
```bash
# Create namespace
kubectl create namespace abs-api
# Run the deployment config "abs-aspnetapi-nodeport.yaml" placed under the kubernetes folder 
kubectl apply -f abs-aspnetapi-nodeport.yaml -n abs-api
# Expose the API over NodePort
kubectl expose deployment aspnetapi --type=NodePort --name=aspnetapi-nodeport -n abs-api
# Test the API
curl http://<node_ip_address>:<nodeport>/api/absdemo
# e.g.
curl http://10.0.0.4:31429/api/absdemo
# Delete namespace
kubectl delete namespace abs-api
```