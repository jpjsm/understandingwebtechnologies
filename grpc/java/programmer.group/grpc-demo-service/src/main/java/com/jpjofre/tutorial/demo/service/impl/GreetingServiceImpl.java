package com.jpjofre.tutorial.demo.service.impl;

import java.util.logging.Logger;

import com.jpjofre.tutorial.demo.service.HelloRequest;
import com.jpjofre.tutorial.demo.service.HelloResponse;
import com.jpjofre.tutorial.demo.service.GreetingService;
import com.jpjofre.tutorial.demo.service.HelloServiceGrpc;

import io.grpc.stub.StreamObserver;

public class GreetingServiceImpl implements HelloServiceGrpc.GreetingService {
/*
	private static final Logger logger = Logger.getLogger(GreetingServiceImpl.class.getName());

	public void sayHello(HelloRequest request, StreamObserver<HelloResponse> responseObserver) {

		logger.info(String.format("sayHello Request parameter information for method calls: name={%s}, id={%d}", request.getName(), request.getId()));

		HelloResponse reply = HelloResponse.newBuilder().setMessage(String.format("Hello, %s", request.getName()))
				.build();
		
		responseObserver.onNext(reply);
		responseObserver.onCompleted();
	}
*/
}