package com.collections.repository;

import java.util.List;

import org.springframework.data.jpa.repository.JpaSpecificationExecutor;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.PagingAndSortingRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.data.rest.core.annotation.RestResource;
import org.springframework.web.bind.annotation.CrossOrigin;

import com.collections.entity.CollectionItem;

@CrossOrigin(origins="*")
public interface CollectionItemRepository extends PagingAndSortingRepository<CollectionItem, Long>, JpaSpecificationExecutor<CollectionItem> {

	@RestResource(path = "byCountry", rel = "byCountry")
	public List<CollectionItem> findByCountry(@Param("cntry") String country);
	
	@Query("SELECT ci FROM CollectionItem ci WHERE ci.country = :cntry")
	public List<CollectionItem> findByCountryQuery(@Param("cntry") String country);

}
