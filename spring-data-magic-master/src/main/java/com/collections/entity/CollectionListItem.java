package com.collections.entity;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.data.rest.core.config.Projection;

@Projection(name = "collectionListItem", types = { CollectionItem.class })
public interface CollectionListItem {
	
	String getName();
	
	@Value("#{target.summary}. Created in #{target.year}")
	String getSummary();
	
	String getCountry();
	
	Short getYear();
	
	Image getSmallImage();

}
