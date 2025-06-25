package com.collections.control;

import static com.collections.repository.CollectionItemSpecs.*;
import static org.springframework.hateoas.server.mvc.WebMvcLinkBuilder.methodOn;
import static org.springframework.hateoas.server.mvc.WebMvcLinkBuilder.linkTo;

import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.data.rest.webmvc.RepositoryRestController;
import org.springframework.hateoas.CollectionModel;
import org.springframework.hateoas.EntityModel;
import org.springframework.http.ResponseEntity;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import com.collections.entity.CollectionItem;
import com.collections.repository.CollectionItemRepository;

@CrossOrigin(origins="*")
@RepositoryRestController
@RequestMapping("/collectionItems")
public class SearchHATEOASController {
	
	@Autowired
	CollectionItemRepository collectionItemRepository;
	
	@Transactional
	@RequestMapping(method = RequestMethod.GET, value = "/hateoassearch/byParams") 
	@ResponseBody ResponseEntity<CollectionModel<EntityModel<CollectionItem>>> searchByParams(@RequestParam(name="cntry", required=false) String country, @RequestParam(required=false) Short year, @RequestParam(required=false) String ... topics) {
		Specification<CollectionItem> byParamsSpec = null;
		if (country != null) {
	    	byParamsSpec = filterByCountry(country);
		}
	    if (year != null) {
	    	byParamsSpec = byParamsSpec != null ? byParamsSpec.and(filterByYear(year))  : filterByYear(year);
		}
	    if (topics != null) {
	    	byParamsSpec = byParamsSpec != null ? byParamsSpec.and(filterByTopics(topics))  : filterByTopics(topics);
		}
		List<CollectionItem> filteredItems = collectionItemRepository.findAll(byParamsSpec);
		List<EntityModel<CollectionItem>> itemResources = StreamSupport.stream(filteredItems.spliterator(), false)
				.map(item -> EntityModel.of(item,
						linkTo(SearchHATEOASController.class).slash(item.getId()).withSelfRel()))
				.collect(Collectors.toList());
		return ResponseEntity.ok(CollectionModel.of(
				itemResources,
				linkTo(methodOn(SearchHATEOASController.class).searchByParams(country, year, topics)).withSelfRel()));
	}
}
