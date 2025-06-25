package com.collections.repository;

import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.get;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.jsonPath;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;

import com.collections.repository.CollectionItemRepository;

@RunWith(SpringRunner.class)
@SpringBootTest
@AutoConfigureMockMvc
public class CollectionItemEndpointTest {
	
	@Autowired
	CollectionItemRepository collectionItemRepository;
	
	@Autowired
	private MockMvc mockMvc;
	
	@Test
	public void testFindAll() throws Exception {
		mockMvc.perform(get("/collectionItems"))
		.andExpect(status().isOk())
		.andExpect(jsonPath("$._embedded.collectionItems[0]").exists())
		.andExpect(jsonPath("$._embedded.collectionItems[0].name").value("The Penny Black"))
		.andExpect(jsonPath("$._embedded.collectionItems[1]").exists())
		.andExpect(jsonPath("$._embedded.collectionItems[1].name").value("Juke"))
		.andExpect(jsonPath("$._embedded.collectionItems[2]").exists())
		.andExpect(jsonPath("$._embedded.collectionItems[2].name").value("Juggling Juke"));
	}
}