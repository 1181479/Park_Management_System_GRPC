# # Use the official k6 image from Load Impact
# FROM grafana/k6

# # Set the working directory in the container
# WORKDIR /app

# # Copy the k6 script to the working directory
# COPY *.js /app/
# COPY ./Protos /app/Protos
# COPY ./Protos2 /app/Protos2
# COPY grpctests.sh /app/


# # RUN ls
# # RUN cd Protos && ls 
# # RUN cd .. && grpctests.sh

# # Set the command to execute the k6 script
# # ENTRYPOINT ["/bin/sh", "grpctests.sh"]
# ENTRYPOINT ["k6", "run", "grpcAllParks.js --out csv=grpcAllParks.csv"]
# # ENTRYPOINT ["k6", "run", "--out csv=grpcAllParks.csv grpcAllParks.js"]
# # ENTRYPOINT ["ls"]



# # Use a Debian-based slim image as the base
# FROM debian:buster-slim

# # Install necessary dependencies (such as curl and other tools)
# RUN apt-get update \
#     && apt-get install -y --no-install-recommends \
#        ca-certificates \
#        wget \
#     && rm -rf /var/lib/apt/lists/*

# # Install K6
# RUN wget https://github.com/grafana/k6/releases/download/v0.51.0/k6-v0.51.0-linux-amd64.deb && dpkg -i k6-v0.51.0-linux-amd64.deb
# WORKDIR /app

# # Display version and verify installation
# # RUN k6 -v
# # RUN ls

# # Default command when the container starts
# CMD ["bash"]
# # , "grpctests.sh"]