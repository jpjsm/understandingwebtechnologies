package com.collections;

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

@RunWith(SpringRunner.class)
@SpringBootTest
@AutoConfigureMockMvc
public class SpringDataMagicApplicationTest {
	
	@Autowired
	private MockMvc mockMvc;

	@Test
	public void testSearchByParams() throws Exception {
		//No customer filters
		mockMvc.perform(get("/collectionItems/hateoassearch/byParams"))
		.andExpect(status().isOk())
		.andExpect(jsonPath("$._embedded.collectionItems[0]").exists())
		.andExpect(jsonPath("$._embedded.collectionItems[1]").exists())
		.andExpect(jsonPath("$._embedded.collectionItems[2]").exists())
		;
		//Filter by country
		mockMvc.perform(get("/collectionItems/hateoassearch/byParams").param("cntry", "us"))
		.andExpect(status().isOk())
		.andExpect(jsonPath("$._embedded.collectionItems[0]").exists())
		.andExpect(jsonPath("$._embedded.collectionItems[1]").exists())
		;
		//Filter by country, year, and topics
		mockMvc.perform(get("/collectionItems/hateoassearch/byParams").param("cntry", "us").param("year", "1996").param("topics", Topics.ARTS.getName(), Topics.PROGRAMMING.getName()))
		.andExpect(status().isOk())
		.andExpect(jsonPath("$._embedded.collectionItems[0].name").value("Juke"))
		.andExpect(jsonPath("$._embedded.collectionItems[1]").doesNotExist())
		;
	}

}
