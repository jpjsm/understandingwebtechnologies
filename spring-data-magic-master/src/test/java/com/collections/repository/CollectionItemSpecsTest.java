package com.collections.repository;

import static com.collections.repository.CollectionItemSpecs.*;
import java.util.List;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import com.collections.Topics;
import com.collections.entity.CollectionItem;

@RunWith(SpringRunner.class)
@SpringBootTest
public class CollectionItemSpecsTest {
	
	@Autowired
	CollectionItemRepository collectionItemRepository;
	
	@Test
	public void testFilterByCountrySpec() {
		List<CollectionItem> resList = collectionItemRepository.findAll(filterByCountry("uk"));
		Assert.assertEquals(1, resList.size());
		Assert.assertEquals("The Penny Black", resList.get(0).getName());
	}
	
	@Test
	public void testFilterByYearSpec() {
		List<CollectionItem> resList = collectionItemRepository.findAll(filterByYear((short) 1840));
		Assert.assertEquals(1, resList.size());
		Assert.assertEquals("The Penny Black", resList.get(0).getName());
	}
	
	@Test
	public void testFilterByTopicsSpec() {
		List<CollectionItem> resList = collectionItemRepository.findAll(filterByTopics(Topics.ARTS.getName(), Topics.PROGRAMMING.getName()));
		Assert.assertEquals(2, resList.size());
		Assert.assertEquals("Juke", resList.get(0).getName());
	}
	
	@Test
	public void testFilterByCountryYearTopics() {
		List<CollectionItem> resList = collectionItemRepository.findAll(filterByCountry("us").and(filterByYear((short) 2000)).and(filterByTopics(Topics.ARTS.getName(), Topics.PROGRAMMING.getName())));
		Assert.assertEquals(1, resList.size());
		Assert.assertEquals("Juggling Juke", resList.get(0).getName());
	}

}
