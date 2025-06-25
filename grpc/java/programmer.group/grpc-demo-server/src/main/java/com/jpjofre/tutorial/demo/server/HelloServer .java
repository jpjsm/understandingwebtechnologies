package com.jpjofre.tutorial.demo.server;

import java.io.IOException;
import java.util.logging.Logger;

import com.jpjofre.tutorial.demo.service.HelloServiceGrpc;
import com.jpjofre.tutorial.demo.service.impl.GreetingServiceImpl;

import io.grpc.Server;
import io.grpc.ServerBuilder;

public class HelloServer {

	private static final Logger logger = Logger.getLogger(HelloServer.class.getName());

	private static final int DEFAULT_PORT = 50051;

	private Server server = null;

	private void start() throws IOException {

		server = ServerBuilder.forPort(DEFAULT_PORT).addService(HelloServiceGrpc.bindService(new GreetingServiceImpl()))
				.build().start();

		logger.info("Server started, listening on " + DEFAULT_PORT);

		Runtime.getRuntime().addShutdownHook(new Thread() {
			@Override
			public void run() {
				System.err.println("*** shutting down gRPC server since JVM is shutting down");
				HelloServer.this.stop();
				System.err.println("*** server shut down");
			}
		});
	}

	private void stop() {
		if (server != null) {
			server.shutdown();
		}
	}

	/**
	 * Await termination on the main thread since the grpc library uses daemon
	 * threads.
	 */
	private void blockUntilShutdown() throws InterruptedException {
		if (server != null) {
			server.awaitTermination();
		}
	}

	public static void main(String[] args) throws IOException, InterruptedException {
		final HelloServer server = new HelloServer();
		server.start();
		server.blockUntilShutdown();
	}

}