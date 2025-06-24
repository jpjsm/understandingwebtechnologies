package com.collections;

public enum Topics {
	
	NATURE(1, "Nature"),
	ARTS(2, "Arts"),
	SPORT(3, "Sport"),
	HISTORY(4, "History"),
	PROGRAMMING(5, "Programming");
	
	private Long id;
	
	private String name;
	
	Topics(long id, String name) {
		this.id = id;
		this.name = name;
	}

	public Long getId() {
		return id;
	}

	public String getName() {
		return name;
	}

}
