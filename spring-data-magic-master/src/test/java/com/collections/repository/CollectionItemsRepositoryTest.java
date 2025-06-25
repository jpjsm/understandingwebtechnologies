package com.collections.repository;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.List;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.transaction.annotation.Transactional;

import com.collections.entity.CollectionItem;
import com.collections.repository.CollectionItemRepository;

@RunWith(SpringRunner.class)
@SpringBootTest
public class CollectionItemsRepositoryTest {
	
	@Autowired
	CollectionItemRepository collectionItemRepository;
	
	@Test
	@Transactional
	public void testFindAll() {
		Iterable<CollectionItem> itemData = collectionItemRepository.findAll();
		List<CollectionItem> itemList = new ArrayList<>();
	    itemData.forEach(itemList::add);
	    Assert.assertEquals(3, itemList.size());
	    CollectionItem item = itemList.get(0);
	    Assert.assertEquals(1, item.getId());
	    Assert.assertEquals("The Penny Black", item.getName());
	    Assert.assertEquals("The very first stamp", item.getSummary());
	    Assert.assertEquals(BigDecimal.valueOf(1000), item.getPrice());
	    Assert.assertEquals("uk", item.getCountry());
	    Assert.assertEquals(Short.valueOf((short) 1840), item.getYear());
	}
	
}
